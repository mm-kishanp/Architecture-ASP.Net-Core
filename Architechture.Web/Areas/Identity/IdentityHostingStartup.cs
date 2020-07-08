using System;
using Architecture.DataBase.DataBaseFirst.Models;
using Architecture.DataBase.DataBaseFirst;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(Architechture.Web.Areas.Identity.IdentityHostingStartup))]
namespace Architechture.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<AccountContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("AccountContextConnection")));

            //    services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<AccountContext>();
            //});
        }
    }
}