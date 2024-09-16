
using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace GameZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(
                //  config => config.Filters.Add(new AuthorizeFilter())
                );

            var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection" ?? throw new InvalidOperationException("No Connection String was found"));
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(ConnectionString)
            );

            // add session and make the life time is 10 minutes
            builder.Services.AddSession(config=> config.IdleTimeout = TimeSpan.FromMinutes(10));
            builder.Services.AddScoped<ICategoriesService,CategoriesService>();
            builder.Services.AddScoped<IDevicesService,DevicesService>();
            builder.Services.AddScoped<IGamesService, GamesService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddIdentity<User,IdentityRole>(

                configs =>
                {
                    configs.Password.RequireDigit = false;
                    configs.Password.RequireLowercase = false;
                    configs.Password.RequireUppercase = false;
                    configs.Password.RequireNonAlphanumeric = false;
                }


                ).AddEntityFrameworkStores<AppDbContext>();

            // timeSpan of change in security 

            builder.Services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });



            #region localizationServices

            builder.Services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(JsonStringLocalizerFactory));

                });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG"),
                    new CultureInfo("de-DE")

                };
                options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });



            builder.Services.AddLocalization();
            builder.Services.AddSingleton<LocalizationMiddleware>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

            #endregion

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
                       //MiddeWare
            //app.Use(async(HttpContext,next) =>
            //{
            //    await HttpContext.Response.WriteAsync("Middle 1");
            //    await next.Invoke();
            //    await HttpContext.Response.WriteAsync("Middle 1 return ");
            //});

            //app.Run(async(HttpContext) =>{
            //   await HttpContext.Response.WriteAsync("middle2 ");
            //    // he will terminate her
            //});

            app.UseHttpsRedirection();

            #region localization
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar-EG") // Adding Arabic (Egypt) culture
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"), // Default is English
                SupportedCultures = supportedCultures,  // Cultures for formatting numbers, dates, etc.
                SupportedUICultures = supportedCultures // Cultures for UI localization (texts, etc.)
            };

            // Apply localization settings
            app.UseRequestLocalization(options);

            app.UseStaticFiles();
            app.UseMiddleware<LocalizationMiddleware>(); // Custom middleware (if needed)

            #endregion


            app.UseRouting();


          

            app.UseAuthentication();
            app.UseAuthorization();
            // don't forget the middleware
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
