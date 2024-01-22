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
	public class DapperReportController : ControllerBase
	{
		private readonly IMediator mediator;

		[HttpGet("employee/{employeeId}")]
		
		public async Task<ApiResponse<ExpenseResponse>> GetEmployeeExpensesEmployeeId(int employeeId)
		{
			var operation = new GetEmployeeExpenses(employeeId);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost("employee/bydate/{employeeId}")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<ExpenseResponse>>> GetEmployeeExpensesByDateEmployeeId(int employeeId, [FromBody] DapperRequest model)
		{
			var operation = new GetEmployeeExpensesByDate(employeeId, model);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost("totalpayments")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<ExpenseResponse>>> GetPayments([FromBody] DapperRequest model)
		{
			var operation = new GetTotalPayments(model);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost("approvedexpenses")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<ExpenseResponse>>> GetApprovedExpensesTotals([FromBody] DapperRequest model)
		{
			var operation = new GetApprovedExpensesTotal(model);
			var result = await mediator.Send(operation);
			return result;
		}

		[HttpPost("rejectedexpenses")]
		[Authorize(Roles = "admin")]
		public async Task<ApiResponse<List<ExpenseResponse>>> GetRejectedExpenses([FromBody] DapperRequest model)
		{
			var operation = new GetRejectedExpensesTotal(model);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
