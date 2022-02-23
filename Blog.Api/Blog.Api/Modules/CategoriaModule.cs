using Blog.Api.DA;
using Blog.Api.Models;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.Api.Modules
{
    public class CategoriaModule : NancyModule
    {
        private readonly DACategoria _DA = null;
        public CategoriaModule() : base("/categorias")
        {
            _DA = new DACategoria();
            Get("/v1/{idCatCategorias}", p => ObtenerCategorias(p));
            Delete("/v1/{idCatCategorias}", p => EliminarCategoria(p));
            Post("/v1/", p => GuardarCategoria(p));
        }

        private dynamic EliminarCategoria(dynamic arg)
        {
            try
            {
                int idCatCategorias = arg.idCatCategorias;
                var r = _DA.EliminarCategoria(idCatCategorias);

                return Response.AsJson(r, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic ObtenerCategorias(dynamic arg)
        {
            try
            {
                int idCatCategorias = arg.idCatCategorias;
                var r = _DA.ObtenerCategorias(idCatCategorias);

                return Response.AsJson(r, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic GuardarCategoria(dynamic arg)
        {
            try
            {
                CategoriaModel categoria = new CategoriaModel();
                byte[] imagen = null;
                categoria.Titulo = this.Request.Form.titulo;
                categoria.Descripcion = this.Request.Form.descripcion;
                foreach (var file in this.Request.Files)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.Value.CopyTo(ms);
                        imagen = ms.ToArray();
                    }
                }
                
                categoria.Imagen = imagen == null ? "" : Convert.ToBase64String(imagen, 0, imagen.Length);
                

                var r = _DA.GuardarCategoria(categoria);

                return Response.AsJson(r, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}