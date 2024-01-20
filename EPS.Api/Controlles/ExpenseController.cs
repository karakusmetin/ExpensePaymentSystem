using EPS.Business.Cqrs;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
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

		public async Task<ApiResponse<List<ExpenseResponse>>> Get()
		{
			var operation = new GetAllExpenseQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{id}")]

		public async Task<ApiResponse<ExpenseResponse>> Get(int id)
		{
			var operation = new GetExpenseByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByParameters")]

		public async Task<ApiResponse<List<ExpenseResponse>>> GetByParameter(
			[FromQuery] string? Title,
			[FromQuery] string? CategoryName)
		{
			var operation = new GetExpenseByParameterQuery(Title, CategoryName);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]

		public async Task<ApiResponse<ExpenseResponse>> Post([FromBody] ExpenseRequest Account)
		{
			var operation = new CreateExpenseCommand(Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]

		public async Task<ApiResponse> Put(int id, [FromBody] ExpenseRequest Account)
		{
			var operation = new UpdateExpenseCommand(id, Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]

		public async Task<ApiResponse> Delete(int id)
		{
			var operation = new DeleteExpenseCommand(id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
