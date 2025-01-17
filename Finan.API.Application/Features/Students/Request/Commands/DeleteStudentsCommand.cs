using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.API.Application.Response;
using MediatR;

namespace Finan.API.Application.Features.Students.Request.Commands
{
    public class DeleteStudentsCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
