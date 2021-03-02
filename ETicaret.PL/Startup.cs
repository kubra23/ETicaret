using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETicaret.BL.Repositories.Concretes;
using ETicaret.BL.Repositories.Abstracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ETicaret.BL.Business.Abstracts;
using ETicaret.BL.Business;
using ETicaret.Entities.Authentications;
using ETicaret.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ETicaret.BL.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using ETicaret.BL.Policies.Handles;
using ETicaret.BL.Services;

namespace ETicaret.PL
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextExtension();
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<ETicaretContext>()
              .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/user/SignIn");
                options.Cookie = new CookieBuilder
                {
                    Name = "ETicaretCookie",
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.Always

                };
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = new PathString("/yonetim-paneli/user/accessdenied");
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "769632690149-thuhrvgcf8i2u43hdnjn9llppmihb19q.apps.googleusercontent.com";
                options.ClientSecret = "_auCpN0hRWbgsQXmUuEppV9M";
            })
             .AddFacebook(options =>
             {
                 options.AppId = "464106821187753";
                 options.AppSecret = "eb9e2bbeab7a28ee97cbdbc20935ed97";
             });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("TimeControl", 
                policy => policy.Requirements.Add(new TimeRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, TimeHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CheckLoginTime", policy => policy.Requirements.Add(new LoginTimeRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, LoginTimeHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CheckGobekAdi", policy => policy.RequireClaim("gobekAdi", "elif"));
            });

            services.AddTransient<IKategoriBusiness, KategoriBusiness>();
            services.AddTransient<IMarkaBusiness, MarkaBusiness>();
            services.AddTransient<IUrunBusiness, UrunBusiness>();
            services.AddTransient<IResimBusiness, ResimBusiness>();
            services.AddTransient<IYorumBusiness, YorumBusiness>();
            services.AddTransient<ISepetBusiness, SepetBusiness>();
            services.AddScoped<AuthenticatorService>();

            services.AddTransient<IUserManagerBusiness, UserManagerBusiness>(x => new UserManagerBusiness(services.BuildServiceProvider().GetService<UserManager<AppUser>>(),
               services.BuildServiceProvider().GetService<RoleManager<AppRole>>()));

            services.AddTransient<ISignInManagerBusiness, SignInManagerBusiness>(x => new SignInManagerBusiness(services.BuildServiceProvider().GetService<SignInManager<AppUser>>(),
                services.BuildServiceProvider().GetService<UserManager<AppUser>>()));
            services.AddScoped<AuthenticatorService>();

            services.AddTransient<IRoleBusiness, RoleBusiness>(x => new RoleBusiness(services.BuildServiceProvider().GetService<RoleManager<AppRole>>()));
            services.AddTransient<ISepetBusiness, SepetBusiness>(x => new SepetBusiness(services.BuildServiceProvider().GetService<UserManager<AppUser>>()));
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("YonetimPaneliUrunVitrinDegistirRoute", "yonetimPaneli", "yonetim-paneli/urunvitrindegistir/{urunId}/{resimId}",
                    new { controller = "Urun", action = "UrunVitrinDegistir" });
                endpoints.MapAreaControllerRoute("YonetimPaneliRoute", "yonetimPaneli", "yonetim-paneli/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("KategoriRoute", "kategori/{kategoriId}", new { controller = "Urun", action = "Index" });
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
