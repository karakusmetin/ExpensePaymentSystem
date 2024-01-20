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
	public class ExpenditureDemandController : ControllerBase
	{
		private readonly IMediator mediator;

		public ExpenditureDemandController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]

		public async Task<ApiResponse<List<ExpenditureDemandResponse>>> Get()
		{
			var operation = new GetAllExpenditureDemandQuery();
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("{id}")]

		public async Task<ApiResponse<ExpenditureDemandResponse>> Get(int id)
		{
			var operation = new GetExpenditureDemandByIdQuery(id);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpGet("ByParameters")]

		public async Task<ApiResponse<List<ExpenditureDemandResponse>>> GetByParameter(
			[FromQuery] string? EmployeeFirstName,
			[FromQuery] string? EmployeeLastName,
			[FromQuery] string? Title)
		{
			var operation = new GetExpenditureDemandByParameterQuery(EmployeeFirstName, EmployeeLastName, Title);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost]

		public async Task<ApiResponse<ExpenditureDemandResponse>> Post([FromBody] ExpenditureDemandRequest Account)
		{
			var operation = new CreateExpenditureDemandCommand(Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("{id}")]

		public async Task<ApiResponse> Put(int id, [FromBody] ExpenditureDemandRequest Account)
		{
			var operation = new UpdateExpenditureDemandCommand(id, Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPut("admin/{id}")]

		public async Task<ApiResponse> PutAdmin(int id, [FromBody] ExpenditureDemandAdminRequest Account)
		{
			var operation = new AdminUpdateExpenditureDemandCommand(id, Account);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpDelete("{id}")]

		public async Task<ApiResponse> Delete(int id)
		{
			var operation = new DeleteExpenditureDemandCommand(id);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
