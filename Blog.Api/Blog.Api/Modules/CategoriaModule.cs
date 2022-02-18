using Blog.Api.DA;
using Blog.Api.Models;
using Nancy;
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
            Get("/v1/{idCatCategorias}", p => GetCategorias(p));
            Post("/v1/", p => PostCategorias(p));
        }

        private dynamic GetCategorias(dynamic arg)
        {
            try
            {
                int idCatCategorias = arg.idCatCategorias;
                var r = _DA.GetCategoria(idCatCategorias);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic PostCategorias(dynamic arg)
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
                if (imagen != null)
                {
                    categoria.Imagen = imagen == null ? "" : Convert.ToBase64String(imagen, 0, imagen.Length);
                }


                var r = _DA.PostCategoria(categoria);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}