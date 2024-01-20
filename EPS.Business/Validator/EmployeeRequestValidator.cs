using EPS.Schema;
using FluentValidation;

namespace EPS.Business.Validator
{
	public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
	{
		public EmployeeRequestValidator()
		{
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage("Username is required")
				.MaximumLength(15).WithMessage("Username cannot be longer than 15 characters");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required")
				.Length(6, 15).WithMessage("Password must be between 6 and 20 characters");

			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("First name is required")
				.MaximumLength(15).WithMessage("First name cannot be longer than 15 characters");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("Last name is required")
				.MaximumLength(15).WithMessage("Last name cannot be longer than 15 characters");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("Invalid email address");

			RuleFor(x => x.IsActive)
				.NotNull().WithMessage("IsActive property must be provided");
		}
	}
}
