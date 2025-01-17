using AutoMapper;
using Finan.API.Application.Contracts.Persistence.CrossRepositories;
using Finan.API.Application.DTOs.Common;
using Finan.API.Application.Features.Students.Request.Commands;
using Finan.API.Application.Response;
using Finan.API.Domain.Estudents;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class LoginStudentsCommandHandler : IRequestHandler<LoginStudentsCommand, BaseCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public LoginStudentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration; // Ya está disponible aquí
    }

    public async Task<BaseCommandResponse> Handle(LoginStudentsCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        try
        {
            var student = await _unitOfWork.Students.GetByEmailAsync(request.Email);

            if (student == null || student.Password != request.Password)
            {
                response.Success = false;
                response.Message = "Invalid email or password.";
                return response;
            }

            var token = GenerateJwtToken(student);

            response.Success = true;
            response.Message = "Login successful.";
            response.Parameters = new List<ParameterDto>
            {
                new ParameterDto { Name = "Token", Value = token }
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Error during login.";
            response.Errors = new List<string> { ex.Message };
        }

        return response;
    }

    private string GenerateJwtToken(StudentsEntity student)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, student.Email),
            new Claim("StudentId", student.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
