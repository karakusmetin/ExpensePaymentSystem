using EPS.Schema;
using ESP.Base.Response;
using MediatR;

namespace EPS.Business.Cqrs
{
	public record CreateExpenditureDemandCommand(ExpenditureDemandRequest Model) : IRequest<ApiResponse<ExpenditureDemandResponse>>;
	public record UpdateExpenditureDemandCommand(int Id, ExpenditureDemandRequest Model) : IRequest<ApiResponse>;
	public record AdminUpdateExpenditureDemandCommand(int Id, ExpenditureDemandAdminRequest Model) : IRequest<ApiResponse>;
	public record DeleteExpenditureDemandCommand(int Id) : IRequest<ApiResponse>;


	public record GetAllExpenditureDemandQuery() : IRequest<ApiResponse<List<ExpenditureDemandResponse>>>;
	public record GetExpenditureDemandByIdQuery(int Id) : IRequest<ApiResponse<ExpenditureDemandResponse>>;
	public record GetExpenditureDemandByParameterQuery(string EmployeeFirstName, string EmployeeLastName, string Title) : IRequest<ApiResponse<List<ExpenditureDemandResponse>>>;

}
