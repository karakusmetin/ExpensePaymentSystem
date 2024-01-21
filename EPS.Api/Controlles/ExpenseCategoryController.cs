using EPS.Business.Cqrs;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPS.Api.Controlles
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExpenseCategoryController : ControllerBase
	{
		private readonly IMediator mediator;

		public ExpenseCategoryController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Get()
		{
			var operation = new GetAllExpenseCategoryQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse<ExpenseCategoryResponse>> Get(int id)
		{
			var operation = new GetExpenseCategoryByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<ExpenseCategoryResponse>> Post([FromBody] ExpenseCategoryRequest Account)
		{
			var operation = new CreateExpenseCategoryCommand(Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse> Put(int id, [FromBody] ExpenseCategoryRequest Account)
		{
			var operation = new UpdateExpenseCategoryCommand(id, Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse> Delete(int id)
		{
			var operation = new DeleteExpenseCategoryCommand(id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
