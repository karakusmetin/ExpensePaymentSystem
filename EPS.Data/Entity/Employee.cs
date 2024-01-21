using ESP.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entity
{
	[Table("Employee", Schema = "dbo")]
	public class Employee : UserBaseEntity
	{
		public string Role { get; set; } = "employee";
		public int ExpensRequestCount { get; set; }


        public virtual List<Expense> Expenses { get; set; }
    }

	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.UpdateDate).IsRequired(false);
			builder.Property(x => x.UpdateUserId).IsRequired(false);
			builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

			builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.Password).IsRequired(true).HasMaxLength(250);
			builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.Role).IsRequired(true).HasDefaultValue("employee");
			builder.Property(x => x.Email).IsRequired(true);
			builder.Property(x => x.LastActivityDate).IsRequired(false);
			builder.Property(x => x.PasswordRetryCount).IsRequired(false).HasDefaultValue(0);
		}
	}
}
