﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter.Deserialization;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Collections.Generic;
using System.Linq;

using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.DataContracts;


using UserTenantTestAPI.Models;
using UserTenantTestAPI.Controllers;


namespace UserTenantTestAPI
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
            services.AddDbContext<UserStoreContext>(opt => opt.UseCosmos(Configuration["CosmosDB:URL"], Configuration["CosmosDB:PrimaryKey"], Configuration["DatabaseId"]));
            services.AddControllers();
            services.AddOData(opt => opt.AddModel("odata", GetEdmModel())
                .AddModel("v{version}", GetEdmModel())
                .Count().Filter().Select().Expand().OrderBy().SetMaxTop(1));

            // To avoid looping .
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Application Insights Settings 
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);
            services.PostConfigureAll<LoggerFilterOptions>(action =>
            {
                var matchingRule = action.Rules.SingleOrDefault(

                    x => x.ProviderName == typeof(ApplicationInsightsLoggerProvider).FullName);
                if (matchingRule != null)
                {
                    action.Rules.Remove(matchingRule);
                }
            });
            services.AddSingleton<ITelemetryInitializer, UserTenantTestAPI.TelemetryClassAdvance>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseODataBatching();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<User>("Users");
            return builder.GetEdmModel();
        }
    }
}
