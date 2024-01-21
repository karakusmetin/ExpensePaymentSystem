using EPS.Schema;
using ESP.Base.Response;
using MediatR;

namespace EPS.Business.Cqrs
{
	public record GetEmployeeExpenses(int employeeId) : IRequest<ApiResponse<List<ExpenseResponse>>>;
	public record GetEmployeeExpensesByDate(int employeeId, DapperRequest Model) : IRequest<ApiResponse<List<ExpenseResponse>>>;
	public record GetTotalPayments(DapperRequest Model) : IRequest<ApiResponse<List<ExpenseResponse>>>;
	public record GetApprovedExpensesTotal(DapperRequest Model) : IRequest<ApiResponse<List<ExpenseResponse>>>;
	public record GetRejectedExpensesTotal(DapperRequest Model) : IRequest<ApiResponse<List<ExpenseResponse>>>;
}
