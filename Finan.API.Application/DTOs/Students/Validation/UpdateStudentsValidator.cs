using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Finan.API.Application.DTOs.Students.Validation
{
    public class UpdateStudentsValidator : AbstractValidator<StudentDto>
    {
        public UpdateStudentsValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
