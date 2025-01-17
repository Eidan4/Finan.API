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
    public class DeleteStudentsCommandHandler : IRequestHandler<DeleteStudentsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteStudentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteStudentsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var validator = new DeleteStudentsValidator();
                ValidationResult validatorResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validatorResult.IsValid)
                {
                    var firstError = validatorResult.Errors.FirstOrDefault()?.ErrorMessage;
                    throw new Exception($"Failed to Send: {firstError}");
                }

                // Buscar el usuario por ID
                var user = await _unitOfWork.Students.GetByIdAsync(request.Id);
                _ = user == null ? throw new Exception("User not found") : true;

                // Eliminar el usuario
                _unitOfWork.Students.Delete(user);
                await _unitOfWork.CompleteAsync();

                // Configurar respuesta exitosa
                response.Success = true;
                response.Message = "User deleted successfully";
            }
            catch (Exception ex)
            {
                // Configurar respuesta en caso de error
                response.Success = false;
                response.Message = "Error deleting user";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
