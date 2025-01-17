using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Finan.API.Application.DTOs.Students.Validation
{
    public class CreateStudentsValidator : AbstractValidator<StudentDto>
    {
        public CreateStudentsValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Phone)
               .NotEmpty().WithMessage("Phone is required");

            RuleFor(x => x.Picture)
               .NotEmpty().WithMessage("Picture is required");
        }
    }
}
