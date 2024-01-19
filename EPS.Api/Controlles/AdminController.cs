using EPS.Business.Cqrs;
using EPS.Schema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESP.Base.Response;

namespace EPS.Api.Controlles
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IMediator mediator;

		public AdminController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]
		
		public async Task<ApiResponse<List<AdminResponse>>> Get()
		{
			var operation = new GetAllAdminQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{id}")]
		
		public async Task<ApiResponse<AdminResponse>> Get(int id)
		{
			var operation = new GetAdminByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByParameters")]

		public async Task<ApiResponse<List<AdminResponse>>> GetByParameter(
			[FromQuery] string? FirstName,
			[FromQuery] string? LastName)
		{
			var operation = new GetAdminByParameterQuery(FirstName, LastName);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]
		
		public async Task<ApiResponse<AdminResponse>> Post([FromBody] AdminRequest Account)
		{
			var operation = new CreateAdminCommand(Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]
		
		public async Task<ApiResponse> Put(int id, [FromBody] AdminRequest Account)
		{
			var operation = new UpdateAdminCommand(id, Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]
		
		public async Task<ApiResponse> Delete(int id)
		{
			var operation = new DeleteAdminCommand(id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
