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
	public class ExpenditureDemandController : ControllerBase
	{
		private readonly IMediator mediator;

		public ExpenditureDemandController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse<List<ExpenditureDemandResponse>>> Get()
		{
			var operation = new GetAllExpenditureDemandQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse<ExpenditureDemandResponse>> Get(int id)
		{
			var stringuserId = HttpContext.User.FindFirst("Id")?.Value;
			int.TryParse(stringuserId, out int userId);

			var operation = new GetExpenditureDemandByIdQuery(id, userId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByParameters")]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse<List<ExpenditureDemandResponse>>> GetByParameter(
			[FromQuery] string? EmployeeFirstName,
			[FromQuery] string? EmployeeLastName,
			[FromQuery] string? Title)
		{
			var stringuserId = HttpContext.User.FindFirst("Id")?.Value;
			int.TryParse(stringuserId, out int userId);

			var operation = new GetExpenditureDemandByParameterQuery(EmployeeFirstName, EmployeeLastName, Title, userId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse<ExpenditureDemandResponse>> Post([FromBody] ExpenditureDemandRequest Account)
		{
			var stringuserId = HttpContext.User.FindFirst("Id")?.Value;
			int.TryParse(stringuserId, out int userId);

			var operation = new CreateExpenditureDemandCommand(Account, userId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin,employee")]

		public async Task<ApiResponse> Put(int id, [FromBody] ExpenditureDemandRequest Account)
		{
			var stringuserId = HttpContext.User.FindFirst("Id")?.Value;
			int.TryParse(stringuserId, out int userId);

			var operation = new UpdateExpenditureDemandCommand(id, Account, userId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("admin/{id}")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse> PutAdmin(int id, [FromBody] ExpenditureDemandAdminRequest Account)
		{
			var stringuserId = HttpContext.User.FindFirst("Id")?.Value;
			int.TryParse(stringuserId, out int userId);

			var operation = new AdminUpdateExpenditureDemandCommand(id, Account,userId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]

		public async Task<ApiResponse> Delete(int id)
		{
			var stringuserId = HttpContext.User.FindFirst("Id")?.Value;
			int.TryParse(stringuserId, out int userId);

			var operation = new DeleteExpenditureDemandCommand(id,userId);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
