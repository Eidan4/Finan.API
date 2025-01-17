using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finan.API.Application.Contracts.Persistence.CrossRepositories;
using Finan.API.Application.DTOs.Students.Validation;
using Finan.API.Application.Features.Students.Request.Commands;
using Finan.API.Application.Response;
using MediatR;
using FluentValidation.Results;

namespace Finan.API.Application.Features.Students.Handlers.Commands
{
    public class UpdateStudentsCommandHandler : IRequestHandler<UpdateStudentsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStudentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateStudentsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                // Validar los datos del DTO
                var validator = new UpdateStudentsValidator();
                ValidationResult validationResult = await validator.ValidateAsync(request.studentDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.Message = "Validation failed";
                    response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                // Buscar el usuario existente
                var user = await _unitOfWork.Students.GetByIdAsync((int)request.studentDto.Id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    response.Errors = new List<string> { "The specified user does not exist." };
                    return response;
                }

                // Mapear los datos del DTO a la entidad User
                _mapper.Map(request.studentDto, user);

                // Actualizar la fecha de modificación
                user.UpdatedDate = DateTime.UtcNow;

                // Guardar los cambios en la base de datos
                _unitOfWork.Students.Update(user);
                await _unitOfWork.CompleteAsync();

                // Configurar la respuesta exitosa
                response.Success = true;
                response.Message = "User updated successfully";
            }
            catch (Exception ex)
            {
                // Configurar respuesta en caso de error
                response.Success = false;
                response.Message = "Error updating user";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
