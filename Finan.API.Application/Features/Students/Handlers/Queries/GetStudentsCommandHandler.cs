using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Finan.API.Application.Contracts.Persistence.CrossRepositories;
using Finan.API.Application.DTOs.Common;
using Finan.API.Application.DTOs.Students;
using Finan.API.Application.Features.Students.Request.Queries;
using Finan.API.Application.Response;
using MediatR;

namespace Finan.API.Application.Features.Students.Handlers.Queries
{
    public class GetStudentsCommandHandler : IRequestHandler<GetStudentsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStudentsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(GetStudentsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                // Obtener estudiantes desde el repositorio
                var students = await _unitOfWork.Students.GetAllAsync();

                // Filtrar por número de documento si está presente en el request
                if (!string.IsNullOrEmpty(request.Documents))
                {
                    students = students.Where(s => s.Documents == request.Documents).ToList();
                }

                // Mapear a DTOs (si es necesario)
                var mappedStudents = _mapper.Map<List<StudentDto>>(students);

                // Configurar la respuesta exitosa
                response.Success = true;
                response.Message = "Students retrieved successfully";
                response.Parameters = new List<ParameterDto>
                {
                    new ParameterDto
                    {
                        Name = "Students",
                        Value = Newtonsoft.Json.JsonConvert.SerializeObject(mappedStudents)
                    }
                };
            }
            catch (Exception ex)
            {
                // Configurar respuesta en caso de error
                response.Success = false;
                response.Message = "Error retrieving students";
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
