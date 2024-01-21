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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ESP.Base.TokenJwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

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
			
			services.AddSwaggerGen();

			services.AddControllers().AddFluentValidation(x =>
			{
				x.RegisterValidatorsFromAssemblyContaining<AdminRequestValidator>();
			});

			services.AddEndpointsApiExplorer();

			services.AddResponseCaching();
			services.AddMemoryCache();
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Expense Payment System Api Management", Version = "v1.0" });

				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "Vb Management for IT Company",
					Description = "Enter JWT Bearer token **_only_**",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};
				c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{ securityScheme, new string[] { } }
			});
			});

			JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
			services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = jwtConfig.Issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
					ValidAudience = jwtConfig.Audience,
					ValidateAudience = false,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(2)
				};
			});


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
