using EPS.Data.Enums;
using EPS.Schema;
using FluentValidation;

namespace EPS.Business.Validator
{
	public class ExpenditureDemandRequestValidator : AbstractValidator<ExpenditureDemandRequest>
	{
		public ExpenditureDemandRequestValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.MaximumLength(15)
				.WithMessage("Title cannot be empty and must be less than 15 characters.");
			
		
			RuleFor(x => x.Amount)
				.GreaterThan(0)
				.WithMessage("Amount must be greater than 0.");
			
			RuleFor(x => x.Description)
				.NotEmpty()
				.WithMessage("Description cannot be empty.")
				.MaximumLength(300)
				.WithMessage("Description must be less than 300 characters.");
				
			
			RuleFor(x => x.Location)
				.NotEmpty().MaximumLength(25)
				.WithMessage("Location cannot be empty and must be less than 25 characters.");
			
			RuleFor(x => x.DocumentUrl)
				.MaximumLength(255)
				.WithMessage("DocumentUrl must be less than 255 characters.");
		}
	}

	public class ExpenditureDemandAdminRequestValidator : AbstractValidator<ExpenditureDemandAdminRequest>
	{
		public ExpenditureDemandAdminRequestValidator()
		{
			RuleFor(x => x.Id)
				.GreaterThan(0)
				.WithMessage("Id must be greater than 0.");
			
			RuleFor(x => x.IsApproved)
			.Must(value => Enum.TryParse<ExpenditureDemandStatus>(value, true, out _))
			.WithMessage("Invalid IsApproved value.");
			
			RuleFor(x => x.RejectionReason)
				.MaximumLength(255) // Opsiyonel
				.WithMessage("RejectionReason must be less than 255 characters.");
		}
	}
}
