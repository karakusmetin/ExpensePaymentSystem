using EPS.Data.Entity;
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
		}
	}
}
