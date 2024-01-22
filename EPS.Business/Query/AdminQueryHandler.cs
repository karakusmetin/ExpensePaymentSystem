using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data;
using EPS.Data.Entity;
using EPS.Schema;
using LinqKit;
using MediatR;
using ESP.Base.Response;
using Microsoft.EntityFrameworkCore;

namespace EPS.Business.Query
{
	public class AdminQueryHandler :
		IRequestHandler<GetAllAdminQuery, ApiResponse<List<AdminResponse>>>,
		IRequestHandler<GetAdminByIdQuery, ApiResponse<AdminResponse>>,
		IRequestHandler<GetAdminByParameterQuery, ApiResponse<List<AdminResponse>>>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

		public AdminQueryHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<ApiResponse<List<AdminResponse>>> Handle(GetAllAdminQuery request, CancellationToken cancellationToken)
		{

			var adminlist = await dbContext.Set<Admin>().OrderByDescending(x => x.IsActive).ToListAsync(cancellationToken);
			
			var mappedList = mapper.Map<List<Admin>, List<AdminResponse>>(adminlist);
			return new ApiResponse<List<AdminResponse>>(mappedList);
		}

		public async Task<ApiResponse<AdminResponse>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
		{
			var adminEntity = await dbContext.Set<Admin>()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			if (adminEntity == null)
			{
				return new ApiResponse<AdminResponse>("Record not found");
			}

			var mapped = mapper.Map<Admin, AdminResponse>(adminEntity);
			return new ApiResponse<AdminResponse>(mapped);
		}

		public async Task<ApiResponse<List<AdminResponse>>> Handle(GetAdminByParameterQuery request, CancellationToken cancellationToken)
		{
			var predicate = PredicateBuilder.New<Admin>(false); // varsayılan olarak false

			if (!string.IsNullOrEmpty(request.FirstName))
			{
				predicate = predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
			}

			if (!string.IsNullOrEmpty(request.LastName))
			{
				predicate = predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
			}

			var list = await dbContext.Set<Admin>()
				.Where(predicate)
				.ToListAsync(cancellationToken);

			var mappedList = mapper.Map<List<Admin>, List<AdminResponse>>(list);
			return new ApiResponse<List<AdminResponse>>(mappedList);
		}
	}
}
