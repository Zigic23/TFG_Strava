using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;
using StravaConnector.Objects;
using StravaConnector.RestManagers;
using StravaTrainingGenerator.Models.Configuration.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace StravaTrainingGenerator.Models.Configuration.OAuth
{
    public class StravaOAuthHandler : OAuthHandler<StravaOAuthOptions>
    {
        private IServiceProvider ServiceProvider;
        private ConnectionStrings connectionStrings;
        public StravaOAuthHandler(IOptions<ConnectionStrings> connectionStrings, IOptionsMonitor<StravaOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider) : base(options, logger, encoder, clock)
        {
            this.connectionStrings = connectionStrings.Value;
            this.ServiceProvider = serviceProvider;
        }

        public override async Task<bool> HandleRequestAsync()
        {
            bool ajaxRequest = Request.Headers.ContainsKey("X-Requested-With") && Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            string authorization_code = Request.Query.ContainsKey("code") ? Request.Query["code"].ToString() : null;
            if (authorization_code != null)
            {
                //Con la autorización, federamos al usuario
                StravaAccessToken response = new OAuthRestManager(Options.StravaSettings.strava_url).GetToken(authorization_code, Options.StravaSettings.client_id, Options.StravaSettings.client_secret, Options.StravaSettings.grant_type);
                if (response != null)
                {
                    CookieOptions cookieOptions = new CookieOptions();
                    //Asignamos un tiempo de expiración a la cookie
                    cookieOptions.Expires = DateTimeOffset.Now.AddMilliseconds(response.expires_in);
                    //Le decimos que la cookie es esencial
                    cookieOptions.IsEssential = true;
                    cookieOptions.HttpOnly = true;
                    Response.Cookies.Append("t", response.access_token, cookieOptions);
                    return await Task.FromResult(false); //Comprobación hecha, no hace falta seguir
                }
            }
            else if (Request.Path.Value.ToLower().StartsWith("/login") || Request.Path.Value.ToLower().StartsWith("/errores") || Request.Path.Value.Equals("/") || Request.Path.Value.Contains("Content"))
            {
                return await Task.FromResult(false);
            }
            else if (Request.Cookies.ContainsKey("t"))
            {
                //Recuperar cookie y recuperar identidad de usuario
                authorization_code = Request.Cookies["t"];

                //Options.UserInformationEndpoint Se debería utilizar esto, pero como ya existe en el SDK una llamada... pues la usamos.
                Athlete response = Request.HttpContext.Session.HasValue(SessionKeys.UserKey) ? Request.HttpContext.Session.Get<Athlete>(SessionKeys.UserKey) : new AthletesManager(Options.StravaSettings.strava_url).GetLoggedAthlete(authorization_code);
                if (response != null)
                {
                    //Asignar al Identity
                    ClaimsIdentity identity = new System.Security.Claims.ClaimsIdentity(new List<Claim>()
                    {
                        new Claim("name", response.firstname),
                        new Claim("idSTRAVA", response.id.ToString())
                    }, "Custom");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await Context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddSeconds(36000)
                    });
                    Context.User.AddIdentity(identity);//Tenemos que añadir el authenticationType para que IsAuthenticated este a true

                    //Guardamos en sesión el objeto de usuario
                    Request.HttpContext.Session.Set(SessionKeys.UserKey, response);

                    return await Task.FromResult(false); //Comprobación hecha
                }
                else
                {
                    //Eliminamos de sesión el objeto de usuario
                    Request.HttpContext.Session.Remove(SessionKeys.UserKey);
                    Response.HttpContext.Session.Set(SessionKeys.ErrorMessageKey, "Error al conectar con base de datos");
                    if (ajaxRequest)
                        Response.StatusCode = StatusCodes.Status401Unauthorized;
                    else
                        Response.Redirect("/errores");
                    return await Task.FromResult(true);
                }
            }

            //Redirect to login
            if (ajaxRequest)
                Response.StatusCode = StatusCodes.Status401Unauthorized;
            else
            {
                string url = BuildRedirectUri($"/login");
                Response.Redirect(url);
            }

            await Context.SignOutAsync();
            //return base.HandleRequestAsync();
            return await Task.FromResult(true);
        }
    }
}
