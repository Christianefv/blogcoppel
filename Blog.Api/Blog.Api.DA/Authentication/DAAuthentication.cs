using Blog.Api.Models;
using Blog.Api.Models.Authentication;
using Blog.Api.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace Blog.Api.DA.Authentication
{
    public class DAAuthentication
    {
        private readonly Conexion _conexion = null;

        public DAAuthentication()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<UsuarioModel> Login(CredencialesModel credenciales)
        {
            var r = new Result<UsuarioModel>();

            var parametros = new ConexionParameters();
            parametros.Add("@pUsuario", ConexionDbType.VarChar, credenciales.Usuario);
            parametros.Add("@pPassword", ConexionDbType.VarChar, credenciales.Password);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var result = _conexion.RecordsetsExecute("ProcCatUsuariosValida", parametros);
            if (result)
            {
                UsuarioModel usuario = new UsuarioModel();
                usuario = _conexion.RecordsetsResults<UsuarioModel>()?.FirstOrDefault();
                r.Data = usuario;
                r.Value = parametros.Value("@pResultado").ToBoolean();
                r.Message = parametros.Value("@pMsg").ToString();
            }
            else
            {
                r.Value = false;
                r.Message = "Credenciales incorrectas.";
            }


            return r;
        }

        public Result<List<CredencialesModel>> CredencialesDifarmer()
        {
            var result = new Result<List<CredencialesModel>>();

            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            result = _conexion.ExecuteWithResults<CredencialesModel>("procObtenerCredencialesDifarmer", parametros);

            return result;
        }
    }
}
