using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finan.API.Application.Contracts.Persistence.CrossRepositories;
using Finan.API.Application.DTOs.Common;
using Finan.API.Application.DTOs.Students;
using Finan.API.Application.DTOs.Students.Validation;
using Finan.API.Application.Features.Students.Request.Commands;
using Finan.API.Application.Response;
using Finan.API.Domain.Estudents;
using MediatR;
using AutoMapper;
using FluentValidation.Results;


namespace Finan.API.Application.Features.Students.Handlers.Commands
{
    public class CreateStudentsCommandHandler : IRequestHandler<CreateStudentsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateStudentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateStudentsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                // Validar el DTO
                var validator = new CreateStudentsValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request.studentDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.Message = "Validation failed";
                    response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                var user = _mapper.Map<StudentsEntity>(request.studentDto);

                // Establecer fechas de creación y modificación
                user.CreatedDate = DateTime.UtcNow;
                user.UpdatedDate = DateTime.UtcNow;

                // Agregar el usuario a la base de datos
                await _unitOfWork.Students.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                // Mapear el usuario recién creado a un DTO
                var createdUserDto = _mapper.Map<StudentDto>(user);

                // Configurar la respuesta exitosa
                response.Success = true;
                response.Message = "User created successfully";
                response.Parameters = new List<ParameterDto>
                {
                    new ParameterDto
                    {
                        Name = "CreatedUser",
                        Value = Newtonsoft.Json.JsonConvert.SerializeObject(createdUserDto)
                    }
                };
            }
            catch (Exception ex)
            {
                // Configurar respuesta en caso de error
                response.Success = false;
                response.Message = "Error creating user";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
