
using JobApplication.Application.DTOs.Auth;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly ICandidateRepository _candidateRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService,
            ICandidateRepository candidateRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _candidateRepository = candidateRepository;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception("Email is already registered.");

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join( ", ",result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            await _userManager.AddToRoleAsync(user, ApplicationRoles.Candidate);
            var candidate = new Candidate
            {
                UserId = user.Id,
                Name = request.Name
            };

            await _candidateRepository.AddAsync(candidate);
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken( user.Id,user.Email, roles);

            return new AuthResponse
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Role = ApplicationRoles.Candidate
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user.Id, user.Email, roles);
            return new AuthResponse
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }
    }
}
