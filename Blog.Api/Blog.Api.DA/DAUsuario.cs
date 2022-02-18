using Blog.Api.Models;
using Blog.Api.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace Blog.Api.DA
{
    public class DAUsuario
    {
        private readonly Conexion _conexion = null;
        public DAUsuario()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result AltaUsuario(UsuarioModel usuario)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pUsuario", ConexionDbType.VarChar, usuario.Usuario);
                parametros.Add("@pNombre", ConexionDbType.VarChar, usuario.Nombre);
                parametros.Add("@pCorreo", ConexionDbType.VarChar, usuario.Correo);
                parametros.Add("@pPassword", ConexionDbType.VarChar, usuario.Password);
                parametros.Add("@pAvatar", ConexionDbType.VarChar, usuario.Avatar);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcCatUsuariosGuarda", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }
    }
}
