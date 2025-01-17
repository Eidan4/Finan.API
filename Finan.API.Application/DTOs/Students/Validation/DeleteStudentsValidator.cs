using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.API.Application.Features.Students.Request.Commands;
using FluentValidation;

namespace Finan.API.Application.DTOs.Students.Validation
{
    public class DeleteStudentsValidator : AbstractValidator<DeleteStudentsCommand>
    {
        public DeleteStudentsValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
