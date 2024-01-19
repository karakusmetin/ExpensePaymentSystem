using ESP.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using EPS.Data.Enums;

namespace EPS.Data.Entity
{
	[Table("Expense", Schema = "dbo")]
	public class Expense : BaseEntityWithId
	{
		public int EmployeeId { get; set; } // Personel ID'si
		public virtual Employee Employee { get; set; }
		
		public int ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        
		
		public decimal Amount { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public string? DocumentUrl { get; set; } // Fatura veya fişin depolandığı URL
		public ExpenseRequestStatus IsApproved { get; set; } // Yönetici tarafından onaylanıp onaylanmadığı
		public string RejectionReason { get; set; } // Reddedilme durumunda nedeni
		public DateTime SubmissionDate { get; set; }
		public DateTime? ApprovalDate { get; set; } // Onay tarihi, eğer onaylandıysa
	}

	public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
	{
		public void Configure(EntityTypeBuilder<Expense> builder)
		{
			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.UpdateDate).IsRequired(false);
			builder.Property(x => x.UpdateUserId).IsRequired(false);
			builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

			builder.Property(x => x.EmployeeId).IsRequired(true);
			builder.Property(x => x.ExpenseCategoryId).IsRequired(true);
			builder.Property(x => x.Amount).IsRequired(true).HasPrecision(18,4);
			builder.Property(x => x.Location).IsRequired(true).HasMaxLength(25);
			builder.Property(x => x.Description).IsRequired(true).HasMaxLength(300);
			builder.Property(x => x.DocumentUrl).IsRequired(false);
			builder.Property(x => x.IsApproved).IsRequired(true).HasDefaultValue(value:ExpenseRequestStatus.Pending);
			builder.Property(x => x.RejectionReason).IsRequired(true).HasMaxLength(250);
			builder.Property(x=>x.SubmissionDate).IsRequired(true);
			builder.Property(x=>x.ApprovalDate).IsRequired(false);

		}
	}
}
