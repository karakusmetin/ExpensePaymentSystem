using EPS.Schema;
using FluentValidation;

namespace EPS.Business.Validator
{
	public class ExpenseCategoryRequestValidator : AbstractValidator<ExpenseCategoryRequest>
	{
		public ExpenseCategoryRequestValidator() 
		{
			RuleFor(x => x.CategoryName)
				.NotEmpty().WithMessage("CategoryName is required")
				.MaximumLength(25).WithMessage("CategoryName cannot be longer than 25 characters");
		}
	}
}
