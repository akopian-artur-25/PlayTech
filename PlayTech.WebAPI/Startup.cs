using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlayTech.Business;
using PlayTech.Business.Services;
using PlayTech.Business.Services.Interfaces;
using PlayTech.Shared.Database;
using PlayTech.Shared.Database.Interfaces;
using PlayTech.Shared.Utils;
using PlayTech.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;

namespace PlayTech.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly IHostEnvironment _environment;

        public Startup(IHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            #region Security

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            #endregion Security


            #region Database

            services.AddDbContext<PlayTechContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDbContext>(provider => provider.GetService<PlayTechContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            #endregion Database


            #region Business

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            #endregion Business


            #region Routing

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllersWithViews(options => options.Filters.Add(typeof(ModelStateFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(options => 
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(new
                    {
                        Errors = actionContext.ModelState.Values
                            .SelectMany(o => o.Errors)
                            .Select(o => o.ErrorMessage)
                    });
                };
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlayTech API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddFluentValidationRules();
            });

            #endregion Routing


            #region Mapping

            services.AddAutoMapper(options => options.AllowNullCollections = true
                , typeof(EntityToDTOMapping)
                , typeof(DTOToEntityMapping)
                , typeof(DTOToVMMapping)
                , typeof(VMToDTOMapping));

            #endregion Mapping

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseRouting();
            
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlayTech API");
                c.RoutePrefix = "docs";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
