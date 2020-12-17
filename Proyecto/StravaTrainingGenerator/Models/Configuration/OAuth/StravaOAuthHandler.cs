using Microsoft.AspNetCore.Authentication;
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

        public override Task<bool> HandleRequestAsync()
        {
            bool ajaxRequest = Request.Headers.ContainsKey("X-Requested-With") && Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            string authorization_code = Request.Query.ContainsKey("authorization_code") ? Request.Query["authorization_code"].ToString() : null;
            if (authorization_code != null)
            {
                //Con la autorización, federamos al usuario
                StravaAccessToken response = new OAuthRestManager(Options.StravaSettings.strava_url).GetToken(Options.StravaSettings.token_url, authorization_code, Options.StravaSettings.client_id, Options.StravaSettings.client_secret, Options.StravaSettings.grant_type);
                if (response != null)
                {
                    CookieOptions cookieOptions = new CookieOptions();
                    //Asignamos un tiempo de expiración a la cookie
                    cookieOptions.Expires = DateTimeOffset.Now.AddMilliseconds(response.expires_in);
                    //Le decimos que la cookie es esencial
                    cookieOptions.IsEssential = true;
                    cookieOptions.HttpOnly = true;
                    Response.Cookies.Append("t", response.access_token, cookieOptions);
                    return Task.FromResult<bool>(false); //Comprobación hecha, no hace falta seguir
                }
            }
            else if (Request.Path.Value.Equals("/login") || Request.Path.Value.Equals("/errores"))
            {
                return Task.FromResult<bool>(false);
            }
            else if (Request.Cookies.ContainsKey("t"))
            {
                //Recuperar cookie y recuperar identidad de usuario
                authorization_code = Request.Cookies["t"];

                //Options.UserInformationEndpoint Se debería utilizar esto, pero como ya existe en el SDK una llamada... pues la usamos.
                Athlete response = new AthletesManager(Options.StravaSettings.strava_url).GetLoggedAthlete(authorization_code);
                if (response != null)
                {
                    UserManager userManager = new UserManager(connectionStrings["ApiBBDDURL"]);
                    //Miramos si tenemos el usuario en session, y si no lo tenemos lo recogemos desde la api.
                    UserData user = Request.HttpContext.Session.HasValue(SessionKeys.UserData) ? Request.HttpContext.Session.Get<Athlete>(SessionKeys.UserData) : userManager.GetUserData(new Guid(response.user.id));
                    if (user != null && user.error == null)
                    {
                        //Asignar al Identity
                        ClaimsIdentity identity = new System.Security.Claims.ClaimsIdentity(new List<Claim>()
                        {
                            new Claim("email", response.user.email),
                            new Claim("name", response.user.name),
                            new Claim("idSIP", response.user.id),
                        }, "Custom");
                        Context.User.AddIdentity(identity);//Tenemos que añadir el authenticationType para que IsAuthenticated este a true

                        //Guardamos en sesión el objeto de usuario
                        Request.HttpContext.Session.Set(SessionKeys.UserData, user);

                        //Obtenemos los permisos del usuario y lo guardamos en sesión
                        List<UserPermission> permisos = userManager.GetUserPermissions(user.Cd_User);
                        Request.HttpContext.Session.Set(SessionKeys.UserPermissions, permisos);

                        if (user.Cd_Empresa.HasValue)
                        {
                            //Si el usuario tiene empresa, nos guardamos el cliente en sesión si no está ya.
                            CompanyData company = Request.HttpContext.Session.HasValue(SessionKeys.CompanyData) ? Request.HttpContext.Session.Get<CompanyData>(SessionKeys.CompanyData) : new CompanyManager(connectionStrings["ApiBBDDURL"]).GetCompany(user.Cd_Empresa.Value);

                            Request.HttpContext.Session.Set(SessionKeys.CompanyData, company);
                        }

                        return Task.FromResult<bool>(false); //Comprobación hecha
                    }

                }
                else
                {
                    //Eliminamos de sesión el objeto de usuario
                    Request.HttpContext.Session.Remove(SessionKeys.UserData);
                    Response.HttpContext.Session.Set(SessionKeys.ErrorMessage, "Error al conectar con base de datos");
                    if (ajaxRequest)
                        Response.StatusCode = StatusCodes.Status401Unauthorized;
                    else
                        Response.Redirect("/errores");
                    return Task.FromResult<bool>(true);
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
            //return base.HandleRequestAsync();
            return Task.FromResult<bool>(true);
        }
    }
}
