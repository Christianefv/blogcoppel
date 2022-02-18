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
    public class DACategoria
    {
        private readonly Conexion _conexion = null;
        public DACategoria()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<CategoriaModel>> GetCategoria(int idCatCategorias)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pIdCatCategorias", ConexionDbType.Int, idCatCategorias);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.ExecuteWithResults<CategoriaModel>("ProcCatCategoriasCon", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result<List<CategoriaModel>>(ex);
            }
        }

        public Result<List<CategoriaModel>> PostCategoria(CategoriaModel categoria)
        {
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pTitulo", ConexionDbType.VarChar, categoria.Titulo);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, categoria.Descripcion);
                parametros.Add("@pImagen", ConexionDbType.VarChar, categoria.Imagen);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                var r = _conexion.ExecuteWithResults<CategoriaModel>("ProcCatCategoriasGuarda", parametros);
                return r;
            }
            catch (Exception ex)
            {
                return new Result<List<CategoriaModel>>(ex);
            }
        }
    }
}
