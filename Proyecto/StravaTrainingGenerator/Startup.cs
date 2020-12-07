using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneMVCCore.Models.Configuration.Settings;
using Microsoft.Extensions.Hosting;

namespace StoneMVCCore
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            //Credenciales de acceso a base de datos
            services.Configure<ConnectionStrings>(options => Configuration.GetSection("ConnectionStrings").Bind(options));
            //Configuración de correo electrónico
            services.Configure<MailSettings>(options => Configuration.GetSection("MailSettings").Bind(options));
            //Configuración de claves de aplicación
            services.Configure<KeysSettings>(options => Configuration.GetSection("KeysSettings").Bind(options));


            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddKendo();

            services.AddSingleton(Configuration);
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
            app.UseFileServer();
            app.UseCookiePolicy();

            app.UseRouting();

            // Add MVC to the request pipeline.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Errores",
                    pattern: "Errores/{action}",
                    defaults: new { controller = "Errores" });
            });
        }
    }
}
