using Blog.Api.DA.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Helpers;
using WarmPack.Web.Nancy.Jwt;
using WarmPack.Web.Nancy.Models.Jwt;

namespace Blog.Api.Modules
{
    public class TokenManager : IRefreshTokenManager
    {
        private readonly DATokens _DATokens;

        public TokenManager()
        {
            _DATokens = new DATokens();
        }

        public object Save(ITokenResponseModel param)
        {
            var token = param as TokenItemModel;

            int codCliente = Convert.ToInt32(token.User.Id);
            var ip = NetworkHelper.GetPublicExtternalIPAddress();

            var r = _DATokens.Guardar(codCliente, token);

            return r.ToString();
        }

        public object Delete(object param)
        {
            string refreshToken = param as string;

            var r = _DATokens.Eliminar(refreshToken);

            return r;
        }

        public ITokenResponseModel Find(object param)
        {
            string refreshToken = param as string;

            var r = _DATokens.Buscar(refreshToken);

            return r.Data?.FirstOrDefault();
        }
    }
}