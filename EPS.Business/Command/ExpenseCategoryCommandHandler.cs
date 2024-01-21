using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data.Entity;
using EPS.Data;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPS.Business.Command
{
	public class ExpenseCategoryCommandHandler :
		IRequestHandler<CreateExpenseCategoryCommand, ApiResponse<ExpenseCategoryResponse>>,
		IRequestHandler<UpdateExpenseCategoryCommand, ApiResponse>,
		IRequestHandler<DeleteExpenseCategoryCommand, ApiResponse>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

		public ExpenseCategoryCommandHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
		{
			var checkExpenseCategory = await dbContext.Set<ExpenseCategory>().Where(x => x.CategoryName == request.Model.CategoryName)
			.FirstOrDefaultAsync(cancellationToken);
			if (checkExpenseCategory != null)
			{
				return new ApiResponse<ExpenseCategoryResponse>($"{request.Model.CategoryName} is already exist");
			}
			var entity = mapper.Map<ExpenseCategoryRequest, ExpenseCategory>(request.Model);
			entity.UpdateDate = DateTime.Now;
			entity.InsertDate = DateTime.Now;
			entity.UpdateUserId = request.UserId;

			var entityResult = await dbContext.AddAsync(entity, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);

			var mapped = mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(entityResult.Entity);
			return new ApiResponse<ExpenseCategoryResponse>(mapped);
		}

		public async Task<ApiResponse> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
		{
			var dbExpenseCategory = await dbContext.Set<ExpenseCategory>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbExpenseCategory == null)
			{
				return new ApiResponse("Record not found");
			}
			dbExpenseCategory.CategoryName = request.Model.CategoryName;
			dbExpenseCategory.UpdateDate= DateTime.UtcNow;
			dbExpenseCategory.UpdateUserId = request.UserId;

			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}

		public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
		{
			var dbExpenseCategory = await dbContext.Set<Admin>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbExpenseCategory == null)
			{
				return new ApiResponse("Record not found");
			}
			dbExpenseCategory.IsActive = false;
			dbExpenseCategory.UpdateDate = DateTime.UtcNow;
			dbExpenseCategory.UpdateUserId = request.UserId;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
	}
}
