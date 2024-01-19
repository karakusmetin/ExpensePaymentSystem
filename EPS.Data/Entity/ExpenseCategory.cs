using ESP.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entity
{
	[Table("ExpenseCategory", Schema = "dbo")]

	public class ExpenseCategory : BaseEntityWithId
	{
        public string CategoryName { get; set; }
    }
	public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
	{
		public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
		{
			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.UpdateDate).IsRequired(false);
			builder.Property(x => x.UpdateUserId).IsRequired(false);
			builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

			builder.Property(x => x.CategoryName).IsRequired(true).HasMaxLength(15);
			
		}
	}
}
