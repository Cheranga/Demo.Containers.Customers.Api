using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.Api.Core;
using Customers.Api.Infrastructure.DataAccess;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Customers.Api
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
            RegisterMediators(services);
            RegisterValidators(services);
            RegisterInfrastructure(services, Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Customers.Api", Version = "v1"}); });
        }

        private void RegisterInfrastructure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            services.AddSingleton(_ =>
            {
                var databaseConfig = configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
                return databaseConfig;
            });
        }

        private void RegisterMediators(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
        }

        private void RegisterValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ModelValidatorBase<>).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
             if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
                 app.UseSwagger();
                 app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers.Api v1"));
                 
                 app.UseHttpsRedirection();
             }
             

             app.UseRouting();

             app.UseAuthorization();

             app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}