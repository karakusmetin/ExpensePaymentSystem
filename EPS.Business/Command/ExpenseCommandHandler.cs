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
	public class ExpenseCommandHandler :
		IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
		IRequestHandler<UpdateExpenseCommand, ApiResponse>,
		IRequestHandler<DeleteExpenseCommand, ApiResponse>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

		public ExpenseCommandHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}
		public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
		{
			var employee = await dbContext.Set<Employee>().Where(x => x.Id == request.Model.EmployeeId)
			.FirstOrDefaultAsync(cancellationToken);

			var checkExpense = await dbContext.Set<Expense>().Where(x => x.Equals(request.Model))
			.FirstOrDefaultAsync(cancellationToken);

			if (checkExpense != null)
				return new ApiResponse<ExpenseResponse>("Expense is already exist.Check expense list!");

			if (employee == null)
				return new ApiResponse<ExpenseResponse>("Employee doesnt exist");

			var entity = mapper.Map<ExpenseRequest, Expense>(request.Model);
			entity.InsertDate = DateTime.UtcNow;
			entity.UpdateDate = DateTime.UtcNow;
			entity.ApprovalDate = DateTime.UtcNow;
			entity.SubmissionDate = DateTime.UtcNow;


			var entityResult = await dbContext.AddAsync(entity, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);

			var mapped = mapper.Map<Expense, ExpenseResponse>(entityResult.Entity);
			return new ApiResponse<ExpenseResponse>(mapped);
		}

		public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
		{
			var dbExpense = await dbContext.Set<Expense>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbExpense == null)
			{
				return new ApiResponse("Record not found");
			}

			if (dbExpense.IsApproved != request.Model.IsApproved)
			{
				dbExpense.ApprovalDate = DateTime.UtcNow;
			}
			mapper.Map(request.Model, dbExpense);
			dbExpense.UpdateDate = DateTime.UtcNow;

			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}

		public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
		{
			var dbExpense = await dbContext.Set<Expense>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbExpense == null)
			{
				return new ApiResponse("Record not found");
			}
			dbExpense.IsActive = false;
			dbExpense.UpdateDate = DateTime.UtcNow;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
	}
}
