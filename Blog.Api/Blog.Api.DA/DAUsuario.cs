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

        public Result<List<UsuarioModel>> ConsultarUsuario(int idCatUsuario)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCatUsuario", ConexionDbType.Int , idCatUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.ExecuteWithResults<UsuarioModel>("ProcCatUsuariosCon", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result<List<UsuarioModel>>(ex);
            }
        }

        public Result GuardarCodigoCorreo(string correo, string codigo)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pCorreo", ConexionDbType.VarChar, correo);
                parametros.Add("@pCodigo", ConexionDbType.VarChar, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcCodigosCorreosGuarda", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }   
        }

        public Result ValidarCodigoCorreo(string correo, string codigo)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pCorreo", ConexionDbType.VarChar, correo);
                parametros.Add("@pCodigo", ConexionDbType.VarChar, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcCodigosCorreosValida", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }
    }
}
