using EPS.Data.Entity;
using ESP.Base.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EPS.Data
{
	public class EPSDbContext : DbContext
	{
		public EPSDbContext(DbContextOptions<EPSDbContext> options):base(options) 
		{
		}

		
		public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenditureDemand> ExpenditureDemands { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AdminConfiguration());
			modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
			modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
			modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new ExpenditureDemandConfiguration());
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Admin>().HasData(
				new Admin
				{
					Id = 1,
					UserName = "admin",
					Password = "14e1b600b1fd579f47433b88e8d85291",
					FirstName = "Metin",
					LastName = "KARAKUŞ",
					Status = 1, // Aktif durum
					Email = "metin@example.com",
					LastActivityDate = DateTime.Now,
					PasswordRetryCount = 0,
					Role = "admin", // Kullanıcı rolü
					InsertDate = DateTime.Now,
					UpdateUserId = null,
					UpdateDate = null,
					IsActive = true
				},
				new Admin
				{
					Id = 2,
					UserName = "admin1",
					Password = "14e1b600b1fd579f47433b88e8d85291",
					FirstName = "Admin",
					LastName = "BigAdmin",
					Status = 1, // Aktif durum
					Email = "admin.user@example.com",
					LastActivityDate = DateTime.Now,
					PasswordRetryCount = 0,
					Role = "admin", // Admin rolü
					InsertDate = DateTime.Now,
					UpdateUserId = null,
					UpdateDate = null,
					IsActive = true
				}
			);
		}
	}
}
