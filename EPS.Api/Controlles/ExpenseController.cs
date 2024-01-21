using EPS.Business.Cqrs;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPS.Api.Controlles
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExpenseController : ControllerBase
	{
		private readonly IMediator mediator;

		public ExpenseController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<List<ExpenseResponse>>> Get()
		{
			var operation = new GetAllExpenseQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse<ExpenseResponse>> Get(int id)
		{
			var operation = new GetExpenseByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByParameters")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<List<ExpenseResponse>>> GetByParameter(
			[FromQuery] string? Title,
			[FromQuery] string? CategoryName)
		{
			var operation = new GetExpenseByParameterQuery(Title, CategoryName);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<ExpenseResponse>> Post([FromBody] ExpenseRequest Account)
		{
			var operation = new CreateExpenseCommand(Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse> Put(int id, [FromBody] ExpenseRequest Account)
		{
			var operation = new UpdateExpenseCommand(id, Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse> Delete(int id)
		{
			var operation = new DeleteExpenseCommand(id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
