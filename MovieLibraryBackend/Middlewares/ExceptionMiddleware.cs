﻿using MovieLibraryApi.Model;

namespace MovieLibraryApi.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Unhandled exception occurred");

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;

			var response = ResponseModel.Fail("An unexpected error occurred.", ex.ToString());

			await context.Response.WriteAsJsonAsync(response);
		}
	}
}

