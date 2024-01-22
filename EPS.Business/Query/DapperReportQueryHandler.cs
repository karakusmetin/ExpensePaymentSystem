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
		IRequestHandler<GetEmployeeExpenses, ApiResponse<ExpenseResponse>>,
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

		public async Task<ApiResponse<ExpenseResponse>> Handle(GetEmployeeExpenses request, CancellationToken cancellationToken)
		{
			var connection = dbContext.Database.GetDbConnection();

			// Parametreli sorgu kullanımı
			var query = "SELECT * FROM Expense WHERE EmployeeId = @EmployeeId";
			var expenses = await connection.QueryAsync<Expense>(query, new { EmployeeId = request.employeeId });

			// Boş bir koleksiyon mu kontrol et
			if (expenses == null || !expenses.Any())
			{
				return new ApiResponse<ExpenseResponse>("KÖTÜ İSTEK");
			}

			// Dönen koleksiyondan sadece bir öğe al
			var expense = expenses.First();

			// Map işlemi için IEnumerable değil, tek bir öğe kullanılmalı
			var mappedExpense = mapper.Map<Expense, ExpenseResponse>(expense);

			return new ApiResponse<ExpenseResponse>(mappedExpense);
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
