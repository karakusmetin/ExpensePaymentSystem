using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data;
using EPS.Data.Entity;
using EPS.Schema;
using ESP.Base.Response;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPS.Business.Query
{
	public class EmployeeQueryHandler:
		IRequestHandler<GetAllEmployeeQuery, ApiResponse<List<EmployeeResponse>>>,
		IRequestHandler<GetEmployeeByIdQuery, ApiResponse<EmployeeResponse>>,
		IRequestHandler<GetEmployeeByParameterQuery, ApiResponse<List<EmployeeResponse>>>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

	public EmployeeQueryHandler(EPSDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}
		public async Task<ApiResponse<List<EmployeeResponse>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
		{

			var Employeelist = await dbContext.Set<Employee>().OrderBy(x => x.IsActive).ToListAsync(cancellationToken);

			var mappedList = mapper.Map<List<Employee>, List<EmployeeResponse>>(Employeelist);
			return new ApiResponse<List<EmployeeResponse>>(mappedList);
		}

		public async Task<ApiResponse<EmployeeResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
		{
			var EmployeeEntity = await dbContext.Set<Employee>()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			if (EmployeeEntity == null)
			{
				return new ApiResponse<EmployeeResponse>("Record not found");
			}

			var mapped = mapper.Map<Employee, EmployeeResponse>(EmployeeEntity);
			return new ApiResponse<EmployeeResponse>(mapped);
		}

		public async Task<ApiResponse<List<EmployeeResponse>>> Handle(GetEmployeeByParameterQuery request, CancellationToken cancellationToken)
		{
			var predicate = PredicateBuilder.New<Employee>(true);

			var list = await dbContext.Set<Employee>()
				.Include(x => x.FirstName)
				.Include(x => x.LastName)
				.Where(predicate).ToListAsync(cancellationToken);

			var mappedList = mapper.Map<List<Employee>, List<EmployeeResponse>>(list);
			return new ApiResponse<List<EmployeeResponse>>(mappedList);
		}
	}
}
