﻿using EPS.Business.Cqrs;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace EPS.Api.Controlles
{
	[Route("api/[controller]")]
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly IMediator mediator;

		public TokenController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost]
		public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
		{
			var operation = new CreateTokenCommand(request);
			var result = await mediator.Send(operation);
			return result;
		}
	}
}
