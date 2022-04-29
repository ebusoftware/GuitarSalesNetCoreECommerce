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
            //Cookie olu�turma k�sm�.
            services.ConfigureApplicationCookie(option => //cookie burada yarat�l�r.
            {
                option.LoginPath = "/Home/";
                option.LogoutPath = "/Login/Logout/";
                option.AccessDeniedPath = "/Login/S�gnIn/"; //yanl�� yere girenler i�in gereklidir. 
                option.SlidingExpiration = true; //session s�resi 20 dk d�r 20 dk boyunca herhangi bir istek gelmezse oturum kapat�l�r. 
                option.ExpireTimeSpan = TimeSpan.FromMinutes(40); //40 dk'l�k bir session olu�tur.

                option.Cookie = new CookieBuilder
                {
                    HttpOnly = true, //cookie'yi sadece http olarak alabiliriz.
                    Name = ".Shopapp.Security.Cookie",
                    SameSite = SameSiteMode.Strict //B'kullan�c�s� A'n�n cookiesine sahip olsa bile onun ad�na i�lem yapamaz bunu yazarsak.
                };


            });
            //IdentityOptions ile olu�turulsayd� bu ayarlar yap�labilirdi.
            //Login ayarlar�
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false; //@ * gibi karakterler i�ermeli mi?

                options.Lockout.MaxFailedAccessAttempts = 10; //10 giri�ten sonra kilitleniyor. 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(0.5); //.5 saniye sonra a��l�r
                options.Lockout.AllowedForNewUsers = true; //�sttekilerle alakal�

                //options.User.AllowedUserNameCharacters = ""; //olmas�n� istedi�iniz kesin karaterrleri yazmal�y�z.

                options.User.RequireUniqueEmail = true; //unique emaail adresleri olsun her kullan�c�n�n 

                options.SignIn.RequireConfirmedEmail = true; //Kay�t olduktan sonra email ile token g�nderecek 
                options.SignIn.RequireConfirmedPhoneNumber = false; //telefon do�rulamas�

            });
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBcon")));
            //Authorize �al��t��� zaman devreye girer. Login/S�gnIn/ sayfas�na y�nlendirir.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/Login/S�gnIn/";
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
