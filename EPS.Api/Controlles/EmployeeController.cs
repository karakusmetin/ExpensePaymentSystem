using EPS.Business.Cqrs;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPS.Api.Controlles
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IMediator mediator;

		public EmployeeController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<List<EmployeeResponse>>> Get()
		{
			var operation = new GetAllEmployeeQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse<EmployeeResponse>> Get(int id)
		{
			var stringuserId = HttpContext.User.FindFirst("Id")?.Value;
			int.TryParse(stringuserId, out int userId);
			
			if (userId == id)
			{
				return new ApiResponse<EmployeeResponse>("User is not authenticated.");
			}
			var operation = new GetEmployeeByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByParameters")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<List<EmployeeResponse>>> GetByParameter(
			[FromQuery] string? FirstName,
			[FromQuery] string? LastName)
		{
			var operation = new GetEmployeeByParameterQuery(FirstName, LastName);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<EmployeeResponse>> Post([FromBody] EmployeeRequest Account)
		{
			var operation = new CreateEmployeeCommand(Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse> Put(int id, [FromBody] EmployeeRequest Account)
		{
			var userId = HttpContext.User.FindFirst("Id")?.Value;
			if (userId != null)
			{
				return new ApiResponse("User is not authenticated.");
			}
			var operation = new UpdateEmployeeCommand(id, Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse> Delete(int id)
		{
			var operation = new DeleteEmployeeCommand(id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
