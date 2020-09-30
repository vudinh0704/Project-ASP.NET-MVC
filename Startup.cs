using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Nexus_Service_Marketing_System
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
            services.AddSession();
            services.AddControllersWithViews();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "web",
                    pattern: "{controller=Web}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminDashboard",
                    pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminAccounts",
                    pattern: "admin/{controller=Accounts}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminBillings",
                    pattern: "admin/{controller=Bills}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminConnections",
                    pattern: "admin/{controller=Connections}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminEquipments",
                    pattern: "admin/{controller=Equipments}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminOrders",
                    pattern: "admin/{controller=Orders}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminPlans",
                    pattern: "admin/{controller=Plans}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminPlansOrders",
                    pattern: "admin/{controller=PlansOrders}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminTracks",
                    pattern: "admin/{controller=Tracks}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminRetailShowrooms",
                    pattern: "admin/{controller=RetailShowrooms}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "adminVendors",
                    pattern: "admin/{controller=Vendors}/{action=Index}/{id?}");
            });
        }
    }
}
