﻿using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data.Entity;
using EPS.Data;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using EPS.Data.Enums;

namespace EPS.Business.Command
{
	public class ExpenditureDemandCommandHandler :
		IRequestHandler<CreateExpenditureDemandCommand, ApiResponse<ExpenditureDemandResponse>>,
		IRequestHandler<UpdateExpenditureDemandCommand, ApiResponse>,
		IRequestHandler<AdminUpdateExpenditureDemandCommand, ApiResponse>,
		IRequestHandler<DeleteExpenditureDemandCommand, ApiResponse>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

		public ExpenditureDemandCommandHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<ApiResponse<ExpenditureDemandResponse>> Handle(CreateExpenditureDemandCommand request, CancellationToken cancellationToken)
		{
			//var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var checkCategory = dbContext.Set<ExpenseCategory>().Where(x => x.CategoryName == request.Model.ExpenseCategory)
				.FirstOrDefaultAsync(cancellationToken);

			var checkUser = dbContext.Set<Employee>().Where(x => x.Id == request.UserId)
				.FirstOrDefaultAsync(cancellationToken);

			var checkExpenditureDemand = await dbContext.Set<ExpenditureDemand>().Include(x => x.Employee.Id == request.UserId).Where(x => x.Equals(request.Model))
			.FirstOrDefaultAsync(cancellationToken);

			if (checkExpenditureDemand != null)
				return new ApiResponse<ExpenditureDemandResponse>("This Expense Request is already exist");

			if (checkCategory == null)
				return new ApiResponse<ExpenditureDemandResponse>("This Category not have");
			if (checkUser == null)
				return new ApiResponse<ExpenditureDemandResponse>("You are not in the Employee!");

			var entity = mapper.Map<ExpenditureDemandRequest, ExpenditureDemand>(request.Model);
			entity.SubmissionDate = DateTime.UtcNow;
			entity.InsertDate = DateTime.UtcNow;
			entity.UpdateDate = DateTime.UtcNow;
			entity.UpdateUserId = request.UserId;
			entity.EmployeeId = request.UserId;
			entity.Employee.ExpensRequestCount += 1;

			var entityResult = await dbContext.AddAsync(entity, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);

			var mapped = mapper.Map<ExpenditureDemand, ExpenditureDemandResponse>(entityResult.Entity);
			return new ApiResponse<ExpenditureDemandResponse>(mapped);
		}

		public async Task<ApiResponse> Handle(UpdateExpenditureDemandCommand request, CancellationToken cancellationToken)
		{
			var dbExpenditureDemand = await dbContext.Set<ExpenditureDemand>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbExpenditureDemand == null)
			{
				return new ApiResponse("Record not found");
			}
			if (dbExpenditureDemand.EmployeeId == request.UserId)
				return new ApiResponse("You cant access anathor user request");

			var entity = mapper.Map<ExpenditureDemandRequest, ExpenditureDemand>(request.Model);
			entity.UpdateDate = DateTime.UtcNow;
			entity.UpdateUserId = request.UserId;


			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}

		public async Task<ApiResponse> Handle(AdminUpdateExpenditureDemandCommand request, CancellationToken cancellationToken)
		{
			var dbExpenditureDemand = await dbContext.Set<ExpenditureDemand>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbExpenditureDemand == null)
			{
				return new ApiResponse("Record not found");
			}

			var entity = mapper.Map<ExpenditureDemandAdminRequest, ExpenditureDemand>(request.Model);
			if (entity.IsApproved == ExpenditureDemandStatus.pending)
			{
				entity.UpdateDate = DateTime.UtcNow;
				await dbContext.SaveChangesAsync(cancellationToken);
				return new ApiResponse();
			}
			else
			{
				var ExpenseEntity = mapper.Map<ExpenditureDemand, Expense>(entity);
				ExpenseEntity.ApprovalDate = DateTime.UtcNow;
				ExpenseEntity.UpdateDate = DateTime.UtcNow;
				ExpenseEntity.InsertDate = DateTime.UtcNow;
				ExpenseEntity.UpdateUserId = request.UserId;


				var entityResult = await dbContext.AddAsync(ExpenseEntity, cancellationToken);

				await dbContext.SaveChangesAsync(cancellationToken);
				return new ApiResponse();
			}
		}

		public async Task<ApiResponse> Handle(DeleteExpenditureDemandCommand request, CancellationToken cancellationToken)
		{
			var dbExpenditureDemand = await dbContext.Set<ExpenditureDemand>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbExpenditureDemand == null)
			{
				return new ApiResponse("Record not found");
			}
			dbExpenditureDemand.IsActive = false;
			dbExpenditureDemand.UpdateUserId = request.UserId;
			dbExpenditureDemand.UpdateDate = DateTime.UtcNow;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
	}
}
