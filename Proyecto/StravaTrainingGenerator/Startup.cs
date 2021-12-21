using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneMVCCore.Models.Configuration.Settings;
using Microsoft.Extensions.Hosting;
using StravaTrainingGenerator.Models.Configuration.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using StravaTrainingGenerator.Models.Configuration.OAuth;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;

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
                options.Secure = CookieSecurePolicy.Always;
            });
            
            //Credenciales de acceso a base de datos
            services.Configure<ConnectionStrings>(options => Configuration.GetSection("ConnectionStrings").Bind(options));
            //Configuración de correo electrónico
            services.Configure<MailSettings>(options => Configuration.GetSection("MailSettings").Bind(options));
            //Configuración de claves de aplicación
            services.Configure<KeysSettings>(options => Configuration.GetSection("KeysSettings").Bind(options));

            //Configuraciones de claves de Strava
            StravaSettings stravaSettings = new StravaSettings();
            Configuration.GetSection("StravaSettings").Bind(stravaSettings);
            services.Configure<StravaSettings>(options => Configuration.GetSection("StravaSettings").Bind(options));


            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton(Configuration);
           
            services.AddAuthentication(options =>
            {
                //Indicamos que vamos a utilizar cookies para autenticar y logar
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //Clave de configuración de flujo OAUTH
                options.DefaultChallengeScheme = "StravaSettings";
            })
                .AddCookie()
                .AddOAuth<StravaOAuthOptions, StravaOAuthHandler>("StravaSettings", options =>
                {

                    options.StravaSettings = stravaSettings;
                    options.ClientId = stravaSettings.client_id;
                    options.ClientSecret = stravaSettings.client_secret;

                    //Ruta a la que llamara el proveedor de identidad tras autenticar al usuario
                    options.CallbackPath = new PathString(stravaSettings.redirect_uri);

                    options.AuthorizationEndpoint = $"{stravaSettings.authorize_url}";
                    options.TokenEndpoint = $"{stravaSettings.strava_url}";

                    options.Scope.Add(stravaSettings.scopes);

                    //Endpoint del proveedor de identidad del que obtener información del usuario autenticado
                    //options.UserInformationEndpoint = $"{sipSettings.URL}{sipSettings.UserInformationEndpoint}";
                    options.ClaimActions.Clear();
                    /*
                        //Match de los atributos del objeto de usuario para la respuesta del UserInformationEndpoint
                        foreach (var elem in sipSettings.ClaimActions)
                        {
                            options.ClaimActions.MapJsonKey(elem.Key, elem.Value);
                        }
                    */
                    options.SaveTokens = true;
                    options.Events = new OAuthEvents
                    {
                        //Definición de la funcionalidad de la identificación del usuario a través de su token de acceso 
                        OnCreatingTicket = async context =>
                        {
                            //Petición
                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization = new AuthenticationHeaderValue("X-Key", context.AccessToken);

                            //Respuesta
                            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            //Parseo de respuesta
                            JsonDocument user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                            context.RunClaimActions(user.RootElement);
                        }
                    };
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseFileServer();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

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
