using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Query;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using ShopManagement.Domain.Services;
using ShopManagement.Infrastructure.InventoryACL;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ServiceHost
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


            services.AddHttpContextAccessor();
            var connectionstring = Configuration.GetConnectionString("LampshadeDb");
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
            ShopManagementBootstrapper.Configure(services, connectionstring);
            CustomerDiscountManagementBootstrapper.Configure(services, connectionstring);
            InventoryManagementBootstrapper.Configure(services, connectionstring);
            BlogManagementBootstrapper.Configure(services, connectionstring);

            CommentManagementBootstrapper.Config(services, connectionstring);
            AccountManagementBootstrapper.Config(services, connectionstring);
            services.AddTransient<IAuthHelper, AuthHelper>();
       
            services.AddTransient<IShopInventoryACL,ShopInventoryACL>();


            services.AddTransient<IFileUploader, FileUploader>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
           
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
             {
                 o.LoginPath = new PathString("/Account");
                 o.LogoutPath = new PathString("/Account");
                 o.AccessDeniedPath = new PathString("/AccessDenied");
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea", builder =>
                builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

                options.AddPolicy("Shop", builder =>
                builder.RequireRole(new List<string> { Roles.Administrator }));

                options.AddPolicy("Shop", builder =>
                builder.RequireRole(new List<string> { Roles.Administrator }));


                options.AddPolicy("Discount", builder =>
                {
                    builder.RequireRole(new List<string>
                    {
                        Roles.Administrator
                    });
                });

                options.AddPolicy("Account", builder =>
                {
                    builder.RequireRole(new List<string> { Roles.Administrator });
                });

            });


            services.AddRazorPages()
                .AddMvcOptions(option=>option.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Administrator", "/", "AdminArea");
                    options.Conventions.AuthorizeAreaFolder("Administrator", "/Shop", "Shop");
                    options.Conventions.AuthorizeAreaFolder("Administrator","/Discounts", "Discount");
                    options.Conventions.AuthorizeAreaFolder("Administrator", "/Accounts", "Account");
                });



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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                // endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
