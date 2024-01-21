using EPS.Schema;
using ESP.Base.Response;
using MediatR;

namespace EPS.Business.Cqrs
{
	public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;
}
