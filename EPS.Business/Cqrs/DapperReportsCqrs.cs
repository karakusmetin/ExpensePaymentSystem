using EPS.Data.Entity;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;

namespace EPS.Business.Cqrs
{
	public record GetEmployeeExpenses(int employeeId) : IRequest<ApiResponse<List<Expense>>>;
	public record GetEmployeeExpensesByDate(int employeeId, DateTime startDate, DateTime endDate) : IRequest<ApiResponse<List<Expense>>>;
	public record GetTotalPayments(DateTime startDate, DateTime endDate) : IRequest<ApiResponse<List<Expense>>>;
	public record GetApprovedExpensesTotal(DateTime startDate, DateTime endDate) : IRequest<ApiResponse<List<Expense>>>;
	public record GetRejectedExpensesTotal(DateTime startDate, DateTime endDate) : IRequest<ApiResponse<List<Expense>>>;
}
