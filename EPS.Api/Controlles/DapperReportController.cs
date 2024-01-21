using EPS.Business.Cqrs;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
		public async Task<IActionResult> GetEmployeeExpenses(int employeeId)
		{
			var operation = new GetEmployeeExpenses(employeeId);
			var result = await mediator.Send(operation);
			return Ok(result);
		}

		[HttpPost("employee/bydate/{employeeId}")]
		public async Task<IActionResult> GetEmployeeExpensesByDate(int employeeId, [FromBody] DapperRequest model)
		{
			var operation = new GetEmployeeExpensesByDate(employeeId, model);
			var result = await mediator.Send(operation);
			return Ok(result);
		}

		[HttpPost("totalpayments")]
		public async Task<IActionResult> GetTotalPayments([FromBody] DapperRequest model)
		{
			var operation = new GetTotalPayments(model);
			var result = await mediator.Send(operation);
			return Ok(result);
		}

		[HttpPost("approvedexpenses")]
		public async Task<IActionResult> GetApprovedExpensesTotal([FromBody] DapperRequest model)
		{
			var operation = new GetApprovedExpensesTotal(model);
			var result = await mediator.Send(operation);
			return Ok(result);
		}

		[HttpPost("rejectedexpenses")]
		public async Task<IActionResult> GetRejectedExpensesTotal([FromBody] DapperRequest model)
		{
			var operation = await GetRejectedExpensesTotal(model);
			var result = await mediator.Send(operation);
			return Ok(result);
		}
	}
}
