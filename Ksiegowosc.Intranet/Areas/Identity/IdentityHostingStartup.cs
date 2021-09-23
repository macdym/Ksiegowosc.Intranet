using System;
using Ksiegowosc.Intranet.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Ksiegowosc.Intranet.Areas.Identity.IdentityHostingStartup))]
namespace Ksiegowosc.Intranet.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<KsiegowoscAuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("KsiegowoscAuthDb")));

                services.AddDefaultIdentity<KsiegowoscIntranetUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                })
                    .AddEntityFrameworkStores<KsiegowoscAuthDbContext>();

            });
        }
    }
}