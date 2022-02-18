using Blog.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Web.Nancy.Models.Jwt;

namespace Blog.Api.DA.Authentication
{
    public class DATokens
    {
        private readonly Conexion _conexion = null;

        public DATokens()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result Guardar(int codCliente, TokenItemModel token)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pCodCliente", ConexionDbType.Int, codCliente);
            parametros.Add("@pAccessToken", ConexionDbType.VarChar, token.AccessToken);
            parametros.Add("@pRefreshToken", ConexionDbType.VarChar, token.RefreshToken);
            parametros.Add("@pExpiresAt", ConexionDbType.DateTime, token.ExpiresAt);
            parametros.Add("@pResultado", ConexionDbType.Bit, ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, ParameterDirection.Output);

            var r = _conexion.Execute("procWebTokensGuardar", parametros);
            r.Data = "";
            return r;
        }

        public Result<List<TokenItemModel>> Buscar(string refreshToken)
        {
            var result = new Result<List<TokenItemModel>>();

            var parametros = new ConexionParameters();
            parametros.Add("@pRefreshToken", ConexionDbType.VarChar, refreshToken);
            parametros.Add("@pResultado", ConexionDbType.Bit, ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, ParameterDirection.Output);

            _conexion.RecordsetsExecute("procWebTokensBuscar", parametros);

            var token = _conexion.RecordsetsResults<TokenItemModel>()?.FirstOrDefault();

            if (token != null)
            {
                _conexion.RecordsetsResults(r => {
                    token.User = new WarmPack.Web.Nancy.Models.Security.UserModel(r["CodCliente"].ToString(), r["CodCliente"].ToString(), r["Nombre"].ToString());
                });

                result.Data = new List<TokenItemModel>() { token };
            }

            return result;
        }

        public Result<List<TokenItemModel>> Eliminar(string refreshToken)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pRefreshToken", ConexionDbType.VarChar, refreshToken);
            parametros.Add("@pResultado", ConexionDbType.Bit, ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, ParameterDirection.Output);

            var r = _conexion.ExecuteWithResults<TokenItemModel>("procWebTokensEliminar", parametros);

            return r;
        }
    }
}
