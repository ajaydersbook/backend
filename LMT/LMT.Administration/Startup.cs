using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMT.IRepositories;
using LMT.IServices;
using LMT.Repositories;
using LMT.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using LMT.Common.Jwt;
using LMT.Administration.IServices;
using LMT.Administration.Services;
using LMT.Administration.IRepositories;
using LMT.Administration.Repositories;
using LMT.Common.IRepositories;
using LMT.Common.Repositories;
using LMT.Common.IServices;
using LMT.Common.Services;

namespace LMT.Administration
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});
			//extensions
			services.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
			services.AddJwt(Configuration);
			services.AddSingleton<IJwtHandler, JwtHandler>();
			services.AddSingleton<IStudentService,StudentService>();
			services.AddSingleton<IStudentRepository,StudentRepository>();
			services.AddSingleton<IUserCreateRepository, UserCreateRepository>();
			services.AddSingleton<IUserCreateService, UserCreateService>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            app.UseHttpsRedirection();
			
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Get}/{id?}");
			});
		}
	}
}
