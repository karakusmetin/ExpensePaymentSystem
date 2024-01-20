using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data.Entity;
using EPS.Data;
using EPS.Schema;
using ESP.Base.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPS.Business.Query
{
	public class ExpenseCategoryQueryHandler :
		IRequestHandler<GetAllExpenseCategoryQuery, ApiResponse<List<ExpenseCategoryResponse>>>,
		IRequestHandler<GetExpenseCategoryByIdQuery, ApiResponse<ExpenseCategoryResponse>>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

		public ExpenseCategoryQueryHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GetAllExpenseCategoryQuery request, CancellationToken cancellationToken)
		{

			var ExpenseCategorylist = await dbContext.Set<ExpenseCategory>().OrderByDescending(x => x.IsActive).ToListAsync(cancellationToken);

			var mappedList = mapper.Map<List<ExpenseCategory>, List<ExpenseCategoryResponse>>(ExpenseCategorylist);
			return new ApiResponse<List<ExpenseCategoryResponse>>(mappedList);
		}

		public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken)
		{
			var ExpenseCategoryEntity = await dbContext.Set<ExpenseCategory>()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			if (ExpenseCategoryEntity == null)
			{
				return new ApiResponse<ExpenseCategoryResponse>("Record not found");
			}

			var mapped = mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(ExpenseCategoryEntity);
			return new ApiResponse<ExpenseCategoryResponse>(mapped);
		}
	}

}
