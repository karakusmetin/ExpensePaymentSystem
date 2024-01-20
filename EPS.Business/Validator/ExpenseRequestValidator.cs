using EPS.Schema;
using FluentValidation;

namespace EPS.Business.Validator
{
	public class ExpenseRequestValidator : AbstractValidator<ExpenseRequest>
	{
		public ExpenseRequestValidator()
		{
			RuleFor(x => x.EmployeeId)
				.GreaterThan(0)
				.WithMessage("EmployeeId must be greater than 0.");

			RuleFor(x => x.EmployeeFirstName)
				.NotEmpty()
				.MaximumLength(50)
				.WithMessage("EmployeeFirstName is required and must be less than 50 characters.");
			
			RuleFor(x => x.EmployeeLastName)
				.NotEmpty().MaximumLength(50)
				.WithMessage("EmployeeLastName is required and must be less than 50 characters.");

			RuleFor(x => x.ExpenseCategory)
				.NotEmpty().MaximumLength(50)
				.WithMessage("ExpenseCategory is required and must be less than 50 characters.");
			
			RuleFor(x => x.Title)
				.NotEmpty().MaximumLength(15)
				.WithMessage("Title is required and must be less than 15 characters.");
			
			RuleFor(x => x.Amount)
				.GreaterThan(0)
				.WithMessage("Amount must be greater than 0.");
			
			RuleFor(x => x.Description)
				.NotEmpty()
				.WithMessage("Description is required.")
				.MaximumLength(300)
				.WithMessage("Description is required and must be less than 300 characters.");

			RuleFor(x => x.Location)
				.NotEmpty()
				.MaximumLength(100)
				.WithMessage("Location is required and must be less than 100 characters.");
			
			RuleFor(x => x.DocumentUrl)
				.MaximumLength(25)
				.WithMessage("DocumentUrl must be less than 25 characters.");

			RuleFor(x => x.IsApproved.ToString().ToLower())
				.IsInEnum()
				.WithMessage("Invalid IsApproved value.Please enter: pending,approved or rejected");


			RuleFor(x => x.RejectionReason)
				.MaximumLength(250)
				.WithMessage("RejectionReason must be less than 250 characters.");
		}
	}

}
