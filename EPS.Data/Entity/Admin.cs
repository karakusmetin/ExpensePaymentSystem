
using ESP.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entity
{
	[Table("Admin", Schema = "dbo")]
	public class Admin : UserBaseEntity
	{
		public string Role { get; set; } = "admin";
	}

	public class AdminConfiguration : IEntityTypeConfiguration<Admin>
	{
		public void Configure(EntityTypeBuilder<Admin> builder)
		{
			builder.Property(x => x.InsertDate).IsRequired(true);
			builder.Property(x => x.InsertUserId).IsRequired(true);
			builder.Property(x => x.UpdateDate).IsRequired(false);
			builder.Property(x => x.UpdateUserId).IsRequired(false);
			builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

			builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.Password).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(15);
			builder.Property(x => x.Email).IsRequired(true);
		}
	}
}
