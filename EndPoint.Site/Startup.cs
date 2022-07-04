using EndPoint.Site.Utilites;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.Carts;
using OnlineShop.Application.Services.Finances.Commands.AddPay;
using OnlineShop.Application.Services.Finances.Queries.GetPay;
using OnlineShop.Application.Services.Finances.Queries.GetPayForAdmin;
using OnlineShop.Application.Services.HomePage.FacadPattern;
using OnlineShop.Application.Services.HomePage.MenuItem;
using OnlineShop.Application.Services.Orders.Commands.AddNewOrder;
using OnlineShop.Application.Services.Orders.Queries.GetOrderForAdmin;
using OnlineShop.Application.Services.Orders.Queries.GetOrderForUser;
using OnlineShop.Application.Services.Products.FacadPattern;
using OnlineShop.Application.Services.Sliders.FacadPattern;
using OnlineShop.Application.Services.Users.FacadPattern;
using OnlineShop.Common;
using OnlineShop.Common.UploadFile;
using OnlineShop.Persistence.Contexts;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy(RoleDto.Admin, policy => policy.RequireRole(RoleDto.Admin));
                options.AddPolicy(RoleDto.Operator, policy => policy.RequireRole(RoleDto.Operator));
                options.AddPolicy(RoleDto.Customer, policy => policy.RequireRole(RoleDto.Customer));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => 
            {
                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = new PathString("/Account/Login");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });
            string connectionString = "Data Source=.;Initial Catalog=EnglishStoreDb;Integrated Security = true";
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(connectionString));
            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IProductFacad, ProductFacad>();
            services.AddScoped<IUserFacad, UserFacad>();
            services.AddScoped<ISliderFacad, SliderFacad>();
            services.AddScoped<IEndpointFacad, EndpointFacad>();
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<IGetMenuItem, GetMenuItem>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICookiesManager, CookiesManager>();
            services.AddScoped<IAddPayService, AddPayService>();
            services.AddScoped<IGetPayService, GetPayService>();
            services.AddScoped<IGetPayForAdmin, GetPayForAdmin>();
            services.AddScoped<IGetOrderForAdminService, GetOrderForAdminService>();
            services.AddScoped<IGetOrderForUserService, GetOrderForUserService>();
            services.AddScoped<IAddNewOrderService, AddNewOrderService>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["Secretkey"];
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
       
            });
        }
    }
}
