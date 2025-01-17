using Finan.API.Application.Features.Students.Request.Commands;
using Finan.API.Application.Features.Students.Request.Queries;
using Finan.API.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseCommandResponse>> CreateUser([FromBody] CreateStudentsCommand createStudentsCommand)
        {
            var response = await _mediator.Send(createStudentsCommand);

            return Ok(response);
        }


        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseCommandResponse>> UpdateUser([FromBody] UpdateStudentsCommand updateStudentsCommand)
        {
            var response = await _mediator.Send(updateStudentsCommand);

            return Ok(response);
        }

        [HttpDelete("DeleteUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseCommandResponse>> DeleteUser(int id)
        {
            var response = await _mediator.Send(new DeleteStudentsCommand { Id = id });

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetStudents([FromQuery] string? documents)
        {
            var response = await _mediator.Send(new GetStudentsCommand { Documents = documents });
            return Ok(response);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseCommandResponse>> Login([FromBody] LoginStudentsCommand loginStudentsCommand)
        {
            var response = await _mediator.Send(loginStudentsCommand);

            if (!response.Success)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}
