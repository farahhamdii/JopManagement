using JobApplication.Application.DTOs.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Applications.Commands.UpdateApp
{
    public class UpdateAppStatusCommand : IRequest
    {
        public int ApplicationId { get; set; }
        public UpdateAppStatusRequest Request { get; set; }
        public string RecruiterId { get; set; }

        public UpdateAppStatusCommand(int applicationId,UpdateAppStatusRequest request,string recruiterId)
        {
            ApplicationId = applicationId;
            Request = request;
            RecruiterId = recruiterId;
        }
    }
}
