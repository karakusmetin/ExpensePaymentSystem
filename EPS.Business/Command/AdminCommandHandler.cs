using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data;
using EPS.Data.Entity;
using EPS.Schema;
using MediatR;
using ESP.Base.Response;
using Microsoft.EntityFrameworkCore;
using ESP.Base.EncriptionExtension;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EPS.Business.Command
{
	public class AdminCommandHandler :
		IRequestHandler<CreateAdminCommand, ApiResponse<AdminResponse>>,
		IRequestHandler<UpdateAdminCommand,ApiResponse>,
		IRequestHandler<DeleteAdminCommand,ApiResponse>
	{
		private readonly EPSDbContext dbContext;
		private readonly IMapper mapper;

		public AdminCommandHandler(EPSDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<ApiResponse<AdminResponse>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
		{
			var checkAdmin = await dbContext.Set<Admin>().Where(x => x.UserName.ToUpper() == request.Model.UserName.ToUpper() && x.Password == request.Model.Password)
			.FirstOrDefaultAsync(cancellationToken);
			if (checkAdmin != null)
			{
				return new ApiResponse<AdminResponse>($"{request.Model.UserName} is used by another Admin.");
			}
			request.Model.Password = Md5Extension.GetHash(request.Model.Password.Trim());
			
			var entity = mapper.Map<AdminRequest, Admin>(request.Model);
			entity.InsertDate = DateTime.Now;
			entity.UpdateDate = DateTime.Now;
			entity.PasswordRetryCount = 0;
			

			var entityResult = await dbContext.AddAsync(entity, cancellationToken);
			await dbContext.SaveChangesAsync(cancellationToken);
			
			var mapped = mapper.Map<Admin, AdminResponse>(entityResult.Entity);
			return new ApiResponse<AdminResponse>(mapped);
		}
		
		public async Task<ApiResponse> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
		{
			var dbAdmin = await dbContext.Set<Admin>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbAdmin == null)
			{
				return new ApiResponse("Record not found");
			}
			dbAdmin.FirstName = request.Model.FirstName;
			dbAdmin.LastName = request.Model.LastName;
			dbAdmin.Email = request.Model.Email;
			dbAdmin.UpdateDate = DateTime.Now;

			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
		
		public async Task<ApiResponse> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
		{
			var dbAdmin = await dbContext.Set<Admin>().Where(x => x.Id == request.Id)
			.FirstOrDefaultAsync(cancellationToken);
			if (dbAdmin == null)
			{
				return new ApiResponse("Record not found");
			}
			dbAdmin.IsActive = false;
			dbAdmin.UpdateDate = DateTime.Now;
			await dbContext.SaveChangesAsync(cancellationToken);
			return new ApiResponse();
		}
	}
}
