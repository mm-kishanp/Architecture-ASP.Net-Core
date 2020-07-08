using Architecture.Web.Configuration;
using Architecture.Web.Models;
using Architecture.BusinessLogic;
using Architecture.BusinessLogic.Authentication;
using Architecture.DataBase.DataBaseFirst;
using Architecture.Interface;
using Architecture.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Architecture.DataBase.DataBaseFirst.Models;
using AutoMapper;
using System;

namespace Architecture.Web
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
            services.AddDbContext<AccountContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("AccountContextConnection")));

            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AccountContext>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<AzureStorageConfig>(Configuration.GetSection("AzureStorageConfig"));
            services.Configure<AWSBucketConfig>(Configuration.GetSection("AWSBucketConfig"));

            // SITE CONFIGURATION
            services.AddScoped<SiteConfiguration>();

            // REPOSITORIES
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IRole, RolesRepository>();
            services.AddScoped<IUserRole, UserRoleRepository>();
            services.AddScoped<ILogEntry, LogEntryRepository>();

            // BUSINESS LOGIN SCOPE
            services.AddScoped<IUsersBL, UsersBL>();
            services.AddScoped<ILogEntryBL, LogEnryBL>();

            // Exception Filter
            services.AddScoped<CustomExceptionFilterAttribute>();
            services.AddScoped<LogConstantFilter>();
            services.AddScoped<IActionFilter, RoleFilter>();

            //// FOR authentication
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.EventsType = typeof(CustomCookieAuthenticationEvents);
            //        options.LoginPath = "/Login/Index/";
            //        options.AccessDeniedPath = "/Account/Forbidden/";
            //    }
            //    //.AddJwtBearer(options => {
            //    //    options.Audience = "http://localhost:5001/";
            //    //    options.Authority = "http://localhost:5000/";
            //    //});
            //    );
            services.AddScoped<CustomCookieAuthenticationEvents>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.LoginPath = "/Identity/Account/Login";
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});

            services.AddDbContext<AdminContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AccountContext context)
        {
            // migrate database changes on startup (includes initial db creation)
            context.Database.Migrate();

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                CheckConsentNeeded = context => true,
                MinimumSameSitePolicy = SameSiteMode.None
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "MyArea",
                  template: "{area:exists}/{controller=Users}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");

            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");
            //});
        }
    }
}