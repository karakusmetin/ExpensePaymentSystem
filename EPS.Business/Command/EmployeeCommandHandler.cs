using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data;
using EPS.Data.Entity;
using EPS.Schema;
using ESP.Base.EncriptionExtension;
using ESP.Base.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EPS.Business.Command
{
	public class EmployeeCommandHandler :
		IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeResponse>>,
		IRequestHandler<UpdateEmployeeCommand, ApiResponse>,
		IRequestHandler<DeleteEmployeeCommand, ApiResponse>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

	public EmployeeCommandHandler(EPSDbContext dbContext, IMapper mapper)
	{
		this.dbContext = dbContext;
		this.mapper = mapper;
	}
		public async Task<ApiResponse<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
		{
			var checkAdmin = await dbContext.Set<Admin>().Where(x => x.UserName.ToUpper() == request.Model.UserName.ToUpper() && x.Password == request.Model.Password)
			.FirstOrDefaultAsync(cancellationToken);
			if (checkAdmin != null)
			{
				return new ApiResponse<EmployeeResponse>($"{request.Model.UserName} is used by another Employee.");
			}
			var entity = mapper.Map<EmployeeRequest, Employee>(request.Model);
			entity.Password = Md5Extension.GetHash(request.Model.Password.Trim());
			entity.InsertDate = DateTime.Now;
			entity.UpdateDate = DateTime.Now;

			var entityResult = await dbContext.AddAsync(entity, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);

			var mapped = mapper.Map<Employee, EmployeeResponse>(entityResult.Entity);
			return new ApiResponse<EmployeeResponse>(mapped);
		}

		public async Task<ApiResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
		{
			var dbEmployee = await dbContext.Set<Employee>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbEmployee == null)
			{
				return new ApiResponse("Record not found");
			}
			dbEmployee.FirstName = request.Model.FirstName;
			dbEmployee.LastName = request.Model.LastName;
			dbEmployee.Email = request.Model.Email;
			dbEmployee.UpdateDate = DateTime.Now;
			dbEmployee.Password = Md5Extension.GetHash(request.Model.Password.Trim());

			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}

		public async Task<ApiResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
		{
			var dbEmployee = await dbContext.Set<Employee>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbEmployee == null)
			{
				return new ApiResponse("Record not found");
			}
			dbEmployee.IsActive = false;
			dbEmployee.UpdateDate = DateTime.Now;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
	}
}
