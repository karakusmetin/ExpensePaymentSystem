using EPS.Data.Enums;
using ESP.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entity
{
	[Table("ExpenseRequest", Schema = "dbo")]

	public class ExpenseRequest : BaseEntityWithId
	{	
		public int ExpenseId { get; set; }
		public Expense Expenses { get; set; }
        
		

		public ExpenseRequestStatus Status { get; set; }
		public string EvaluationComment { get; set; }
	}

	public class ExpenseRequestConfiguration : IEntityTypeConfiguration<ExpenseRequest>
	{
		public void Configure(EntityTypeBuilder<ExpenseRequest> builder)
		{
			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.InsertUserId).IsRequired(true);
			builder.Property(x => x.UpdateDate).IsRequired(false);
			builder.Property(x => x.UpdateUserId).IsRequired(false);
			builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

			builder.Property(x=>x.ExpenseId).IsRequired(true);
			builder.Property(x=>x.Status).IsRequired(true);
			builder.Property(x => x.EvaluationComment).IsRequired(true).HasMaxLength(500);
		}
	}
}
