using Blog.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace Blog.Api.DA
{
    public class DAPublicaciones
    {
        private readonly Conexion _conexion = null;
        public DAPublicaciones()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result PostPublicaciones(PublicacionModel publicacion)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCatUsuarios", ConexionDbType.Int, publicacion.IdCatUsuarios);
                parametros.Add("@pIdCatCategorias", ConexionDbType.Int, publicacion.IdCatCategorias);
                parametros.Add("@pTitulo", ConexionDbType.VarChar, publicacion.Titulo);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, publicacion.Descripcion);
                parametros.Add("@pImagen", ConexionDbType.VarChar, publicacion.Imagen);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcCatPublicacionessGuarda", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result<List<PublicacionModel>> GetPublicaciones(PublicacionModel publicacion)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCatUsuarios", ConexionDbType.Int, publicacion.IdCatUsuarios);
                parametros.Add("@pIdCatCategorias", ConexionDbType.Int, publicacion.IdCatCategorias);
                parametros.Add("@pIdCatPublicaciones", ConexionDbType.Int, publicacion.IdCatPublicaciones);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.ExecuteWithResults<PublicacionModel>("ProcCatPublicacionesCon", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result<List<PublicacionModel>>(ex);
            }
        }

        public Result UpdatePublicaciones(PublicacionModel publicacion)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCatPublicaciones", ConexionDbType.Int, publicacion.IdCatPublicaciones);
                parametros.Add("@pIdCatCategorias", ConexionDbType.Int, publicacion.IdCatCategorias);
                parametros.Add("@pTitulo", ConexionDbType.VarChar, publicacion.Titulo);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, publicacion.Descripcion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcCatPublicacionesModifica", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result DeletePublicaciones(int IdCatPublicaciones)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCAtPublicaciones", ConexionDbType.Int, IdCatPublicaciones);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcCatPublicacionesBorrar", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result<List<ComentarioModel>> ConsultarComentarios(int IdCatPublicaciones)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCAtPublicaciones", ConexionDbType.Int, IdCatPublicaciones);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.ExecuteWithResults<ComentarioModel>("ProcComentariosCon", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result<List<ComentarioModel>>(ex);
            }
        }

        public Result GuardarComentario(ComentarioModel comentario)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCAtPublicacion", ConexionDbType.Int, comentario.IdCatPublicaciones);
                parametros.Add("@pIdCatUsuarios", ConexionDbType.Int, comentario.IdCatUsuarios);
                parametros.Add("@pComentario", ConexionDbType.VarChar, comentario.Comentarios);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcComentariosGuarda", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result EliminarComentario(int idComentario)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdComentario", ConexionDbType.Int, idComentario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.Execute("ProcComentariosBorrar", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }
    }
}
