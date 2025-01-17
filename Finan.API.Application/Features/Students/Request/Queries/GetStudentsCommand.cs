using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.API.Application.Response;
using MediatR;

namespace Finan.API.Application.Features.Students.Request.Queries
{
    public class GetStudentsCommand : IRequest<BaseCommandResponse>
    {
        public string? Documents { get; set; }
    }
}
