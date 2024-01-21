using EPS.Schema;
using ESP.Base.Response;
using MediatR;

namespace EPS.Business.Cqrs
{
	public record CreateExpenditureDemandCommand(ExpenditureDemandRequest Model, int UserId) : IRequest<ApiResponse<ExpenditureDemandResponse>>;
	public record UpdateExpenditureDemandCommand(int Id, ExpenditureDemandRequest Model, int UserId) : IRequest<ApiResponse>;
	public record AdminUpdateExpenditureDemandCommand(int Id, ExpenditureDemandAdminRequest Model, int UserId) : IRequest<ApiResponse>;
	public record DeleteExpenditureDemandCommand(int Id, int UserId) : IRequest<ApiResponse>;


	public record GetAllExpenditureDemandQuery() : IRequest<ApiResponse<List<ExpenditureDemandResponse>>>;
	public record GetExpenditureDemandByIdQuery(int Id, int UserId) : IRequest<ApiResponse<ExpenditureDemandResponse>>;
	public record GetExpenditureDemandByParameterQuery(string EmployeeFirstName, string EmployeeLastName, string Title, int UserId) : IRequest<ApiResponse<List<ExpenditureDemandResponse>>>;

}
