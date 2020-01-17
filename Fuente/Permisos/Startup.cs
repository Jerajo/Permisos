using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Permisos.Común.Persistencia;
using Permisos.Común.Persistencia.Servicios;
using Permisos.SqlServer;
using Permisos.SqlServer.Servicios;
using System;

namespace Permisos
{
	public class Startup
	{
		public Startup(IConfiguration configuration) =>
			Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			services.AddControllers(setupActions =>
				{
					setupActions.ReturnHttpNotAcceptable = true;
				})
				.AddXmlDataContractSerializerFormatters()
				.ConfigureApiBehaviorOptions(setupAction =>
				{
					setupAction.InvalidModelStateResponseFactory = context =>
					{
						var problemDetails =
							new ValidationProblemDetails(context.ModelState)
							{
								Type = "https://courselibrary.com/modelvalidationproblem",
								Title = "One or more model validation errors ocurred.",
								Status = StatusCodes.Status422UnprocessableEntity,
								Detail = "See the error property for details.",
								Instance = context.HttpContext.Request.Path
							};

						problemDetails.Extensions.Add("traceId",
							context.HttpContext.TraceIdentifier);

						return new UnprocessableEntityObjectResult(problemDetails)
						{
							ContentTypes = { "application/problem+json" }
						};
					};
				});

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddTransient<IPermisosContexto, PermisosContexto>();

			services.AddTransient<IUnidadDeTrabajo, UnidadDeTrabajo>();

			services.AddDbContext<PermisosContexto>(options =>
			{
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection"))
				.UseLazyLoadingProxies();
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
			{
				app.UseExceptionHandler("/Permisos/Error");
				app.UseHsts();
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "configuraciónInicial",
					pattern: "{controller=Permisos}/{action=Index}/{id?}");

				endpoints.MapControllerRoute(
					name: "páginaNoEncontrada",
					pattern: "",
					defaults: new { controller = "permisos",
						action = "PáginaNoEncontrada" });
			});
		}
	}
}
