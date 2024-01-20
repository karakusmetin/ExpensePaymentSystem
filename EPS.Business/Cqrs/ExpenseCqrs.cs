using EPS.Schema;
using ESP.Base.Response;
using MediatR;

namespace EPS.Business.Cqrs
{
	public record CreateExpenseCommand(ExpenseRequest Model) : IRequest<ApiResponse<ExpenseResponse>>;
	public record UpdateExpenseCommand(int Id, ExpenseRequest Model) : IRequest<ApiResponse>;
	public record DeleteExpenseCommand(int Id) : IRequest<ApiResponse>;


	public record GetAllExpenseQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
	public record GetExpenseByIdQuery(int Id) : IRequest<ApiResponse<ExpenseResponse>>;
	public record GetExpenseByParameterQuery(string Title,string ExpenseCategory) : IRequest<ApiResponse<List<ExpenseResponse>>>;
}
