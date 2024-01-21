using EPS.Business.Cqrs;
using EPS.Data;
using EPS.Data.Entity;
using EPS.Schema;
using ESP.Base.EncriptionExtension;
using ESP.Base.Entity;
using ESP.Base.Response;
using ESP.Base.TokenJwt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace EPS.Business.Command
{
	public class TokenCommandHandler : IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
	{
		private readonly EPSDbContext dbContext;
		private readonly JwtConfig jwtConfig;

		public TokenCommandHandler(EPSDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig)
		{
			this.dbContext = dbContext;
			this.jwtConfig = jwtConfig.CurrentValue;
		}

		public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
		{
			var user = await GetUserByUsernameAsync(request.Model.UserName, cancellationToken);

			if (user == null)
			{
				return new ApiResponse<TokenResponse>("Invalid user information");
			}

			string hash = Md5Extension.GetHash(request.Model.Password.Trim());

			if (hash != user.Password)
			{
				user.LastActivityDate = DateTime.UtcNow;
				user.PasswordRetryCount++;
				await dbContext.SaveChangesAsync(cancellationToken);
				return new ApiResponse<TokenResponse>("Invalid user information");
			}

			if (user.Status != 1 || user.PasswordRetryCount > 3)
			{
				return new ApiResponse<TokenResponse>("Invalid user status");
			}

			user.LastActivityDate = DateTime.UtcNow;
			user.PasswordRetryCount = 0;
			await dbContext.SaveChangesAsync(cancellationToken);

			string token = Token(user);

			return new ApiResponse<TokenResponse>(new TokenResponse()
			{
				Email = user.Email,
				Token = token,
				ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
			});
		}

		private async Task<UserBaseEntity> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
		{
			var adminUser = await dbContext.Set<Admin>().Where(x => x.UserName == username).FirstOrDefaultAsync(cancellationToken);
			if (adminUser != null)
			{
				adminUser.Role = "admin";
				return adminUser;
			}

			var employeeUser = await dbContext.Set<Employee>().Where(x => x.UserName == username).FirstOrDefaultAsync(cancellationToken);
			employeeUser.Role = "employee";
			return employeeUser;
		}

		private string Token(UserBaseEntity user)
		{
			Claim[] claims = GetClaimsAsync(user);
			var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

			var jwtToken = new JwtSecurityToken(
				jwtConfig.Issuer,
				jwtConfig.Audience,
				claims,
				expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
				signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
			);

			string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			return accessToken;
		}

		private Claim[] GetClaimsAsync(UserBaseEntity user)
		{
			var adminUser = dbContext.Set<Admin>().Where(x => x.UserName == user.UserName).FirstOrDefault();

			var claims = new List<Claim>
			{
				new Claim("Id", user.Id.ToString()),
				new Claim("Email", user.Email),
				new Claim("UserName", user.UserName)
			};

			if (adminUser != null)
			{
				claims.Add(new Claim(ClaimTypes.Role, "admin"));
			}
			else
			{
				claims.Add(new Claim(ClaimTypes.Role, "employee"));
			}

			return claims.ToArray();
		}
	}
}
