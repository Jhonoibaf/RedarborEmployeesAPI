using FluentValidation;
using RedarborEmployees.Application.DTOs;
using RedarborEmployees.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace RedarborEmployees.Application.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[!@#$%^&*(),.?\:{ }|<>]").WithMessage("Password must contain at least one special character.");

             RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.LastLogin)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("LastLogin cannot be in the future.");

            RuleFor(x => x.StatusId)
                .IsInEnum().WithMessage("StatusId must be a valid status.");

        }

    }
}
