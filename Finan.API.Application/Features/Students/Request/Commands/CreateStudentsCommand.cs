using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.API.Application.DTOs.Students;
using Finan.API.Application.Response;
using MediatR;

namespace Finan.API.Application.Features.Students.Request.Commands
{
    public class CreateStudentsCommand : IRequest<BaseCommandResponse>
    {
        public StudentDto studentDto { get; set; }
    }
}
