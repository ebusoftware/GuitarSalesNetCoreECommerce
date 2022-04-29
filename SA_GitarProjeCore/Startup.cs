using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using SA_GitarProjeCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace SA_GitarProjeCore
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
            //Cookie oluþturma kýsmý.
            services.ConfigureApplicationCookie(option => //cookie burada yaratýlýr.
            {
                option.LoginPath = "/Home/";
                option.LogoutPath = "/Login/Logout/";
                option.AccessDeniedPath = "/Login/SýgnIn/"; //yanlýþ yere girenler için gereklidir. 
                option.SlidingExpiration = true; //session süresi 20 dk dýr 20 dk boyunca herhangi bir istek gelmezse oturum kapatýlýr. 
                option.ExpireTimeSpan = TimeSpan.FromMinutes(40); //40 dk'lýk bir session oluþtur.

                option.Cookie = new CookieBuilder
                {
                    HttpOnly = true, //cookie'yi sadece http olarak alabiliriz.
                    Name = ".Shopapp.Security.Cookie",
                    SameSite = SameSiteMode.Strict //B'kullanýcýsý A'nýn cookiesine sahip olsa bile onun adýna iþlem yapamaz bunu yazarsak.
                };


            });
            //IdentityOptions ile oluþturulsaydý bu ayarlar yapýlabilirdi.
            //Login ayarlarý
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false; //@ * gibi karakterler içermeli mi?

                options.Lockout.MaxFailedAccessAttempts = 10; //10 giriþten sonra kilitleniyor. 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(0.5); //.5 saniye sonra açýlýr
                options.Lockout.AllowedForNewUsers = true; //üsttekilerle alakalý

                //options.User.AllowedUserNameCharacters = ""; //olmasýný istediðiniz kesin karaterrleri yazmalýyýz.

                options.User.RequireUniqueEmail = true; //unique emaail adresleri olsun her kullanýcýnýn 

                options.SignIn.RequireConfirmedEmail = true; //Kayýt olduktan sonra email ile token gönderecek 
                options.SignIn.RequireConfirmedPhoneNumber = false; //telefon doðrulamasý

            });
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBcon")));
            //Authorize çalýþtýðý zaman devreye girer. Login/SýgnIn/ sayfasýna yönlendirir.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/Login/SýgnIn/";
                });
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseAuthentication();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
