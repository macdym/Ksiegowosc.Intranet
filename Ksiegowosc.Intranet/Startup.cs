using AspNetCore.Unobtrusive.Ajax;
using Ksiegowosc.Data;
using Ksiegowosc.Intranet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ksiegowosc.Intranet
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
            services.AddUnobtrusiveAjax();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<KsiegowoscDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("KsiegowoscDb")));
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<KsiegowoscIntranetSeeder>();
            services.AddScoped<IKontrachentService, KontrachentService>();
            services.AddScoped<IDokumentService, DokumentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, KsiegowoscIntranetSeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(); 
            app.UseUnobtrusiveAjax();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
