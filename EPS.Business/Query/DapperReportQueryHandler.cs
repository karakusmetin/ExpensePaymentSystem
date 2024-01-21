using EPS.Business.Cqrs;
using EPS.Data;
using EPS.Data.Entity;
using ESP.Base.Response;
using MediatR;
using Dapper;
using Microsoft.EntityFrameworkCore;
using EPS.Schema;
using AutoMapper;

namespace EPS.Business.Query
{
	public class DapperReportQueryHandler:
		IRequestHandler<GetEmployeeExpenses, ApiResponse<List<ExpenseResponse>>>,
		IRequestHandler<GetEmployeeExpensesByDate, ApiResponse<List<ExpenseResponse>>>,
		IRequestHandler<GetTotalPayments, ApiResponse<List<ExpenseResponse>>>,
		IRequestHandler<GetApprovedExpensesTotal, ApiResponse<List<ExpenseResponse>>>,
		IRequestHandler<GetRejectedExpensesTotal, ApiResponse<List<ExpenseResponse>>>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;


		public DapperReportQueryHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetEmployeeExpenses request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			var query = "SELECT * FROM Expense WHERE EmployeeId = @EmployeeId";
			var expenses = await connection.QueryAsync<Expense>(query, new { EmployeeId = request.employeeId });

			var mappedList = mapper.Map<IEnumerable<Expense>, List<ExpenseResponse>>(expenses);

			return new ApiResponse<List<ExpenseResponse>>(mappedList);
		}

		public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetEmployeeExpensesByDate request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			var query = "SELECT * FROM Expense WHERE EmployeeId = @EmployeeId AND ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { EmployeeId = request.employeeId, StartDate = request.Model.startDate, EndDate = request.Model.endDate });
			
			var mappedList = mapper.Map<IEnumerable<Expense>, List<ExpenseResponse>>(expenses);
			
			return new ApiResponse<List<ExpenseResponse>>(mappedList);
		}

		public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetTotalPayments request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			var query = "SELECT * FROM Expense WHERE ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { StartDate = request.Model.startDate, EndDate = request.Model.endDate });

			var mappedList = mapper.Map<IEnumerable<Expense>, List<ExpenseResponse>>(expenses);

			return new ApiResponse<List<ExpenseResponse>>(mappedList);
		}

		public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetApprovedExpensesTotal request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();
			
			var query = "SELECT * FROM Expense WHERE IsApproved = 1 AND ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { StartDate = request.Model.startDate, EndDate = request.Model.endDate });

			var mappedList = mapper.Map<IEnumerable<Expense>, List<ExpenseResponse>>(expenses);

			return new ApiResponse<List<ExpenseResponse>>(mappedList);
		}

		public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetRejectedExpensesTotal request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();

			var query = "SELECT * FROM Expense WHERE IsApproved = 0 AND ExpenseDate BETWEEN @StartDate AND @EndDate";
			var expenses = await connection.QueryAsync<Expense>(query, new { StartDate = request.Model.startDate, EndDate = request.Model.endDate });

			var mappedList = mapper.Map<IEnumerable<Expense>, List<ExpenseResponse>>(expenses);

			return new ApiResponse<List<ExpenseResponse>>(mappedList);
		}
	}
}
