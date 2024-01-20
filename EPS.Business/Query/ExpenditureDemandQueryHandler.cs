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
	public class ExpenditureDemandQueryHandler :
		IRequestHandler<GetAllExpenditureDemandQuery, ApiResponse<List<ExpenditureDemandResponse>>>,
		IRequestHandler<GetExpenditureDemandByIdQuery, ApiResponse<ExpenditureDemandResponse>>,
		IRequestHandler<GetExpenditureDemandByParameterQuery, ApiResponse<List<ExpenditureDemandResponse>>>
	{
		private readonly EPSDbContext dbContext;
	private readonly IMapper mapper;

	public ExpenditureDemandQueryHandler(EPSDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<ApiResponse<List<ExpenditureDemandResponse>>> Handle(GetAllExpenditureDemandQuery request, CancellationToken cancellationToken)
	{

		var expenditureDemandResponselist = await dbContext.Set<ExpenditureDemand>().OrderByDescending(x => x.IsActive).ToListAsync(cancellationToken);

		var mappedList = mapper.Map<List<ExpenditureDemand>, List<ExpenditureDemandResponse>>(expenditureDemandResponselist);
		return new ApiResponse<List<ExpenditureDemandResponse>>(mappedList);
	}

	public async Task<ApiResponse<ExpenditureDemandResponse>> Handle(GetExpenditureDemandByIdQuery request, CancellationToken cancellationToken)
	{
		var ExpenditureDemandEntity = await dbContext.Set<ExpenditureDemand>()
		.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (ExpenditureDemandEntity == null)
		{
			return new ApiResponse<ExpenditureDemandResponse>("Record not found");
		}

		var mapped = mapper.Map<ExpenditureDemand, ExpenditureDemandResponse>(ExpenditureDemandEntity);
		return new ApiResponse<ExpenditureDemandResponse>(mapped);
	}

	public async Task<ApiResponse<List<ExpenditureDemandResponse>>> Handle(GetExpenditureDemandByParameterQuery request, CancellationToken cancellationToken)
	{
		var list = await dbContext.Set<ExpenditureDemand>()
			.Include(x => x.Id)
			.Include(x => x.Employee.FirstName)
			.Include(x => x.Employee.LastName)
			.ToListAsync(cancellationToken);

		var mappedList = mapper.Map<List<ExpenditureDemand>, List<ExpenditureDemandResponse>>(list);
		return new ApiResponse<List<ExpenditureDemandResponse>>(mappedList);
	}
}
}
