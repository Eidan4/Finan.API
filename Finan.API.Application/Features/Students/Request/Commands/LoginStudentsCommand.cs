using MediatR;
using Finan.API.Application.Response;

namespace Finan.API.Application.Features.Students.Request.Commands
{
    public class LoginStudentsCommand : IRequest<BaseCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
