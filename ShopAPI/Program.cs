using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopAPI.Database;
using ShopAPI.Services;
using System.Text;

namespace ShopAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			string jwtPrivateKey = builder.Configuration["JwtSecurityKey"];
			builder.Services.AddAuthentication(s =>
			{
				s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(s =>
			{
				s.RequireHttpsMetadata = false;
				s.SaveToken = true;
				s.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtPrivateKey.ToSha512())),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
				};
			});

			builder.Services.AddDbContext<ProductContext>(x => x.UseNpgsql(builder.Configuration["ConnectionString"]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
			builder.Services.AddTransient<IRepository, Repository>();
			builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>(_ => new TokenGenerator(jwtPrivateKey));


			var app = builder.Build();
			app.Services.GetRequiredService<ProductContext>().Database.Migrate();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthentication();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();

		}
	}
}