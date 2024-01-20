using EPS.Data.Enums;
using ESP.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entity
{
	[Table("ExpenditureDemand", Schema = "dbo")]

	public class ExpenditureDemand : BaseEntityWithId
	{
		public int EmployeeId { get; set; } // Personel ID'si
		public virtual Employee Employee { get; set; }

		public int ExpenseCategoryId { get; set; }
		public virtual ExpenseCategory ExpenseCategory { get; set; }


		public string Title { get; set; }
		public decimal Amount { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
		public DateTime SubmissionDate { get; set; }


		public ExpenditureDemandStatus IsApproved { get; set; } // Yönetici tarafından onaylanıp onaylanmadığı
		public string? RejectionReason { get; set; }
	}

	public class ExpenditureDemandConfiguration : IEntityTypeConfiguration<ExpenditureDemand>
	{
		public void Configure(EntityTypeBuilder<ExpenditureDemand> builder)
		{
			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.UpdateDate).IsRequired(false);
			builder.Property(x => x.UpdateUserId).IsRequired(false);
			builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

			builder.Property(x => x.EmployeeId).IsRequired(true);
			builder.Property(x => x.ExpenseCategoryId).IsRequired(true);
			builder.Property(x => x.Title).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18, 4);
			builder.Property(x => x.Location).IsRequired(true).HasMaxLength(25);
			builder.Property(x => x.Description).IsRequired(true).HasMaxLength(300);
			builder.Property(x => x.DocumentUrl).IsRequired(false);
			builder.Property(x => x.IsApproved).IsRequired(true).HasDefaultValue(value: ExpenditureDemandStatus.Pending);
			builder.Property(x => x.SubmissionDate).IsRequired(true);
			
		}
	}
}
