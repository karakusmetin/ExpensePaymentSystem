using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using EPS.Schema;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace VbApi.Middleware
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate next;

		public ErrorHandlerMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (Exception exception)
			{
				Log.Error(exception, "UnexpectedError");
				Log.Fatal(
					$"Path={context.Request.Path} || " +
					$"Method={context.Request.Method} || " +
					$"Exception={exception.Message}"
				);

				context.Response.Clear();
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";

				var errorResponse = new ErrorResponse
				{
					StatusCode = context.Response.StatusCode,
					Message = "Internal Server Error",
					Exception = exception.Message
				};

				var json = JsonSerializer.Serialize(errorResponse);

				await context.Response.WriteAsync(json);
			}
		}
	}
}
