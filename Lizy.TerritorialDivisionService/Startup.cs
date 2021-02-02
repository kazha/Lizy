using Lizy.TerritorialDivisionService.Configuration;
using Lizy.TerritorialDivisionService.Data;
using Lizy.TerritorialDivisionService.Filters;
using Lizy.TerritorialDivisionService.Infrastructure.Repositories;
using Lizy.TerritorialDivisionService.Infrastructure.Services;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Implementations;
using Lizy.TerritorialDivisionService.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lizy.TerritorialDivisionService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TerritorialDivisionConfiguration>(Configuration.GetSection(TerritorialDivisionConfiguration.Path));

            services.AddControllers(ConfigureControllerOptions)
                .AddJsonOptions(SetJsonOptions);


            services.AddDbContext<RegionDbContext>( builder=> BuildDbContext<RegionDbContext>(builder));

            services.AddScoped<IRegionManager, RegionManager>();
            services.AddScoped<IRegionImportManager, RegionImportManager>();
            services.AddScoped<IRegionRepository, RegionRepository>();

            services.AddScoped<DivisionCacheService>();
            services.AddScoped<DataImporter.DataParser>();

            services.AddMemoryCache();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TerritorialDivision Service v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseExceptionHandler(ConfigureExceptionHandler);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<RegionDbContext>().Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected DbContextOptionsBuilder BuildDbContext<TDataCotext>(DbContextOptionsBuilder builder)
            where TDataCotext: DbContext
        {
            var connectionString = Configuration.GetConnectionString(typeof(TDataCotext).Name);
            builder
                .UseSqlServer(
                    connectionString, 
                    options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                )
                .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));

            return builder;
        }

        protected void ConfigureControllerOptions(MvcOptions options)
        {
            options.Filters.Add<ExceptionFilter>();
        }

        protected void ConfigureExceptionHandler(IApplicationBuilder builder)
        {
            builder.Run(async context =>
            {
                //Ran out of time to do this :/
                //Handle startup errors
                //var handler = context.RequestServices.GetService<ExceptionHandler>();
                //handler.HandleException(context);
                await context.Response.CompleteAsync();
            });
        }

        private void SetJsonOptions(JsonOptions options)
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.IgnoreNullValues = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}
