﻿using EPS.Schema;
using ESP.Base.Response;
using MediatR;

namespace EPS.Business.Cqrs

{
	public record CreateExpenseCategoryCommand(ExpenseCategoryRequest Model, int UserId) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
	public record UpdateExpenseCategoryCommand(int Id, ExpenseCategoryRequest Model, int UserId) : IRequest<ApiResponse>;
	public record DeleteExpenseCategoryCommand(int Id, int UserId) : IRequest<ApiResponse>;


	public record GetAllExpenseCategoryQuery() : IRequest<ApiResponse<List<ExpenseCategoryResponse>>>;
	public record GetExpenseCategoryByIdQuery(int Id) : IRequest<ApiResponse<ExpenseCategoryResponse>>;
}

