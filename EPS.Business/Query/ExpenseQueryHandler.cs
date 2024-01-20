using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data.Entity;
using EPS.Data;
using EPS.Schema;
using ESP.Base.Response;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPS.Business.Query
{
	public class ExpenseQueryHandler :
		IRequestHandler<GetAllExpenseQuery, ApiResponse<List<ExpenseResponse>>>,
		IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>,
		IRequestHandler<GetExpenseByParameterQuery, ApiResponse<List<ExpenseResponse>>>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

		public ExpenseQueryHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}
		public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpenseQuery request, CancellationToken cancellationToken)
		{

			var Expenselist = await dbContext.Set<Expense>().OrderByDescending(x => x.IsActive).ToListAsync(cancellationToken);

			var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(Expenselist);
			return new ApiResponse<List<ExpenseResponse>>(mappedList);
		}

		public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
		{
			var ExpenseEntity = await dbContext.Set<Expense>()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			if (ExpenseEntity == null)
			{
				return new ApiResponse<ExpenseResponse>("Record not found");
			}

			var mapped = mapper.Map<Expense, ExpenseResponse>(ExpenseEntity);
			return new ApiResponse<ExpenseResponse>(mapped);
		}

		public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetExpenseByParameterQuery request, CancellationToken cancellationToken)
		{
			var predicate = PredicateBuilder.New<Expense>(true);

			var list = await dbContext.Set<Expense>()
				.Include(x => x.Title)
				.Include(x => x.ExpenseCategory)
				.Where(predicate).ToListAsync(cancellationToken);

			var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
			return new ApiResponse<List<ExpenseResponse>>(mappedList);
		}
	}
}
