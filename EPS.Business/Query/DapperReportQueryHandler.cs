using EPS.Business.Cqrs;
using EPS.Data;
using EPS.Data.Entity;
using ESP.Base.Response;
using MediatR;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace EPS.Business.Query
{
	public class DapperReportQueryHandler:
		IRequestHandler<GetEmployeeExpenses, ApiResponse<List<Expense>>>,
		IRequestHandler<GetEmployeeExpensesByDate, ApiResponse<List<Expense>>>,
		IRequestHandler<GetTotalPayments, ApiResponse<List<Expense>>>,
		IRequestHandler<GetApprovedExpensesTotal, ApiResponse<List<Expense>>>,
		IRequestHandler<GetRejectedExpensesTotal, ApiResponse<List<Expense>>>
	{
		private readonly EPSDbContext dbContext;


		public DapperReportQueryHandler(EPSDbContext dbContext)
		{
			this.dbContext = dbContext;;
		}

		public async Task<ApiResponse<List<Expense>>> Handle(GetEmployeeExpenses request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			var query = "SELECT * FROM Expenses WHERE EmployeeId = @EmployeeId";
			var expenses = await connection.QueryAsync<Expense>(query, new { EmployeeId = request.employeeId });

			return new ApiResponse<List<Expense>>(expenses.ToList());
		}

		public async Task<ApiResponse<List<Expense>>> Handle(GetEmployeeExpensesByDate request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			var query = "SELECT * FROM Expenses WHERE EmployeeId = @EmployeeId AND ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { EmployeeId = request.employeeId, StartDate = request.startDate, EndDate = request.endDate });
			
			return new ApiResponse<List<Expense>>(expenses.ToList());
		}

		public async Task<ApiResponse<List<Expense>>> Handle(GetTotalPayments request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			var query = "SELECT * FROM Expenses WHERE ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { StartDate = request.startDate, EndDate = request.endDate });

			return new ApiResponse<List<Expense>>(expenses.ToList());
		}

		public async Task<ApiResponse<List<Expense>>> Handle(GetApprovedExpensesTotal request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			
			var query = "SELECT * FROM Expenses WHERE IsApproved = 1 AND ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { StartDate = request.startDate, EndDate = request.endDate });

			return new ApiResponse<List<Expense>>(expenses.ToList());
		}

		public async Task<ApiResponse<List<Expense>>> Handle(GetRejectedExpensesTotal request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();

			var query = "SELECT * FROM Expenses WHERE IsApproved = 0 AND ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { StartDate = request.startDate, EndDate = request.endDate });

			return new ApiResponse<List<Expense>>(expenses.ToList());
		}
	}
}
