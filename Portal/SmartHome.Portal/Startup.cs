using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SmartHome.Portal
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var projectId = Environment.GetEnvironmentVariable("PROJECT_ID");
            var region = Environment.GetEnvironmentVariable("IOT_CORE_REGION");
            var registryId = Environment.GetEnvironmentVariable("IOT_CORE_REGISTRY_ID");

            services.AddSingleton<Domain.Telemetry.IDeviceRepository, Infra.Telemetry.DeviceRepository>(_ =>
                new Infra.Telemetry.DeviceRepository(projectId, region, registryId));
            services.AddSingleton<Domain.Telemetry.ITelemetryRepository, Infra.Telemetry.TelemetryRepository>(_ =>
                new Infra.Telemetry.TelemetryRepository(projectId));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
