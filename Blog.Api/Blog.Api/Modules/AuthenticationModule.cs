using Blog.Api.DA.Authentication;
using Blog.Api.Models.Authentication;
using Blog.Api.Models.Usuario;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WarmPack.Classes;
using WarmPack.Web.Nancy;
using WarmPack.Web.Nancy.Jwt;

namespace Blog.Api.Modules
{
    public class AuthenticationModule : NancyModule
    {
        private readonly DAAuthentication _DAAuthentication = null;
        private readonly DATokens _DATokens = null;

        public AuthenticationModule() : base("/seguridad")
        {
            _DAAuthentication = new DAAuthentication();
            _DATokens = new DATokens();

            // bloque de seguridad aqui
            Post("/login", datos => PostLogin(datos));
            Post("/logout", _ => PostLogout());
            Post("/renovar-token", _ => PostRenovarToken());
            Get("/credencialesdifarmer", _ => GetCredencialesDifarmer());

            JwtManager.RefreshTokenManager = new TokenManager();
        }

        private object PostLogin(dynamic arg)
        {
            try
            {
                var credenciales = this.Bind<CredencialesModel>();

                // validar el usuario aqui, modificar el store que se manda llamar dentro del método Login
                var r = _DAAuthentication.Login(credenciales);

                // insertar el accessToken aqui
                if (r.Value)
                {
                    var usuario = (UsuarioModel)r.Data;

                    var claims = new List<Claim>();
                    claims.Add(new Claim("codUsuario", usuario.IdCatUsuario.ToString()));
                    claims.Add(new Claim("usuario", usuario.Usuario.ToString()));
                    claims.Add(new Claim("nombre", usuario.Nombre));
                    var usr = new WarmPack.Web.Nancy.Models.Security.UserModel(usuario.IdCatUsuario.ToString(), usuario.IdCatUsuario.ToString(), usuario.Nombre);
                    var token = JwtManager.GetJwt(usr, claims);

                    //return Response.AsJson(new Result(r.Value, r.Message, token));
                    return Response.AsJson(new { r.Value, r.Message, r.Data, token }, HttpStatusCode.Accepted);
                }
                return Response.AsJson(new Result(r.Value, r.Message), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex), HttpStatusCode.InternalServerError);
            }

        }

        private object PostLogout()
        {
            var p = this.BindModel();

            string refreshToken = p.refresh_token;

            // borrar el refreshToken para que no se puedan solicitar mas tokens
            //Globales.RefreshTokens.RemoveAll(t => t.Uid == refreshToken);
            JwtManager.RefreshTokenManager.Delete(refreshToken);

            return Response.AsJson(new Result(true, "Se ha completado el comando correctamente"));
        }

        private object PostRenovarToken()
        {
            var p = this.BindModel();
            var usuario = this.BindUser();

            string refreshToken = p.refreshToken;

            var t = JwtManager.RefreshTokenManager.Find(refreshToken) as WarmPack.Web.Nancy.Models.Jwt.TokenItemModel;

            if (t != null)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim("codCliente", t.User.Id));
                //claims.Add(new Claim("subUsuario", usuario.Claim("subUsuario").ToString()));
                //claims.Add(new Claim("sucursal", usuario.Claim("sucursal").ToString()));

                var token = JwtManager.GetJwt(new WarmPack.Web.Nancy.Models.Security.UserModel(t.User.Id, t.User.Id, t.User.Name), claims);

                return Response.AsJson(new Result(true, "", token));
            }
            else
            {
                return Response.AsJson(new Result(false, "Token inválido"), HttpStatusCode.Unauthorized);
            }
        }

        private object GetCredencialesDifarmer()
        {
            try
            {
                var result = _DAAuthentication.CredencialesDifarmer();

                return Response.AsJson(result);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }
    }
}