using AutoMapper;
using EPS.Business.Cqrs;
using EPS.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ESP.Business.Mapper;
using VbApi.Middleware;
using FluentValidation.AspNetCore;
using EPS.Business.Validator;

namespace EPS.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			string connection = Configuration.GetConnectionString("MsSqlConnection");
			services.AddDbContext<EPSDbContext>(options => options.UseSqlServer(connection));

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAdminCommand).GetTypeInfo().Assembly));
			
			var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
			services.AddSingleton(mapperConfig.CreateMapper());

			services.AddControllers();
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPS.Api", Version = "v1" });
			});

			services.AddControllers().AddFluentValidation(x =>
			{
				x.RegisterValidatorsFromAssemblyContaining<AdminRequestValidator>();
			});

			services.AddEndpointsApiExplorer();

			services.AddResponseCaching();
			services.AddMemoryCache();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPS.Api v1"));
			}
			app.UseMiddleware<ErrorHandlerMiddleware>();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(x => { x.MapControllers(); });
		}
	}
}
