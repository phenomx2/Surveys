using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Surveys.Web.Providers
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public AppOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
                throw new ArgumentNullException(nameof(publicClientId), "El identificador no es válido");

            _publicClientId = publicClientId;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin",new []{ "*" });

            //Aquí va la logica real
            if (context.UserName == "libro" && context.Password == "xamarin")
            {
                //Crea y prepara el objeto ClaimsIdentity
                var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, "Adrián Romero"));
                var data = new Dictionary<string, string>
                {
                    {"email", "rgfxadrian@outlook.com"}
                };
                var properties = new AuthenticationProperties(data);
                //Crea un AutheticationTicket con la identidad y las propiedades
                var ticket = new AuthenticationTicket(identity,properties);
                //Valida y autentica
                context.Validated(ticket);
                return Task.FromResult(true);
            }
            else
            {
                context.SetError("access_denied","Acceso denegado");
                return Task.FromResult(false);
            }
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key,property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}