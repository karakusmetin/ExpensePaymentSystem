using EPS.Schema;
using MediatR;
using Vb.Base.Response;

namespace EPS.Business.Cqrs
{
	public record CreateAdminCommand(AdminRequest Model) : IRequest<ApiResponse<AdminResponse>>;
	public record UpdateAdminCommand(int Id, AdminRequest Model) : IRequest<ApiResponse>;
	public record DeleteAdminCommand(int Id) : IRequest<ApiResponse>;
	
	
	public record GetAllAdminQuery() : IRequest<ApiResponse<List<AdminResponse>>>;
	public record GetAdminByIdQuery(int Id) : IRequest<ApiResponse<AdminResponse>>;
	public record GetAdminByParameterQuery(string FirstName,string LastName) : IRequest<ApiResponse<List<AdminResponse>>>;

}
