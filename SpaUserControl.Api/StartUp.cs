using Microsoft.Practices.Unity;
using Owin;
using SpaUserControl.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using SpaUserControl.Startup;
using System.Web;
using System.Web.Http;
using SpaUserControl.Domain.Contracts.Service;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using SpaUserControl.Api.Security;

namespace SpaUserControl.Api
{
    public class StartUp
    {
        //Método obrigatório do Owin.
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //Resolução da Dependency Injector.
            var container = new UnityContainer();
            DependecyResolver.Resolve(container);
            config.DependencyResolver = new UnityResolver(container);

            ConfigureWebApi(config);
            ConfigureOAuth(app, container.Resolve<IUserService>());

            //A aplicação é pública AllowAll.
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        //COnfiguração obrigatória (rotas).
        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
             );

        }

        public void ConfigureOAuth(IAppBuilder app, IUserService service)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new AuthorizationServerProvider(service)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}