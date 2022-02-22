using Blog.Api.DA;
using Blog.Api.Models;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.Api.Modules
{
    public class PublicacionesModule : NancyModule
    {
        private readonly DAPublicaciones _DA = null;
        public PublicacionesModule() : base("/usuarios/publicaciones")
        {
            _DA = new DAPublicaciones();
            Get("/v1/{idCatUsuarios}/{idCatCategorias}/{idCatPublicaciones}", p => GetPublicaciones(p));
            Post("/v1/", p => PostPublicaciones(p));
            Put("/v1/", p => UpdatePublicaciones(p));
            Delete("/v1/{idCatPublicaciones}", p => DeletePublicaciones(p));
            Get("/comentarios/v1/{idCatPublicaciones}", p => ComentariosPublicacion(p));
            Post("/comentarios/v1", p => GuardarComentario(p));
            Delete("/comentarios/v1/{idComentarios}", p => EliminarComentario(p));
        }

        private dynamic EliminarComentario(dynamic arg)
        {
            try
            {
                int idComentarios = arg.idComentarios;
                var r = _DA.EliminarComentario(idComentarios);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {

                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic GuardarComentario(dynamic arg)
        {
            try
            {
                var comentario = this.Bind<ComentarioModel>();
                var r = _DA.GuardarComentario(comentario);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {

                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic ComentariosPublicacion(dynamic arg)
        {
            try
            {
                int IdCatPublicaciones = arg.idCatPublicaciones;
                var r = _DA.ConsultarComentarios(IdCatPublicaciones);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {

                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic DeletePublicaciones(dynamic arg)
        {
            try
            {

                int IdCatPublicaciones = arg.idCatPublicaciones;
                var r = _DA.DeletePublicaciones(IdCatPublicaciones);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }

        }
        private dynamic UpdatePublicaciones(dynamic arg)
        {
            try
            {
                PublicacionModel publicacion = new PublicacionModel();
                byte[] imagen = null;
                publicacion.Titulo = this.Request.Form.titulo;
                publicacion.Descripcion = this.Request.Form.descripcion;
                publicacion.IdCatCategorias = this.Request.Form.idCatCategorias;
                publicacion.IdCatUsuarios = this.Request.Form.idCatUsuarios;
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
                    publicacion.Imagen = imagen == null ? "" : Convert.ToBase64String(imagen, 0, imagen.Length);
                }


                var r = _DA.UpdatePublicaciones(publicacion);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic GetPublicaciones(dynamic arg)
        {
            try
            {
                var publicacion = new PublicacionModel();
                publicacion.IdCatPublicaciones = arg.idCatPublicaciones;
                publicacion.IdCatCategorias = arg.idCatCategorias;
                publicacion.IdCatUsuarios = arg.idCatUsuarios;
                var r = _DA.GetPublicaciones(publicacion);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }

        private dynamic PostPublicaciones(dynamic arg)
        {
            try
            {
                PublicacionModel publicacion = new PublicacionModel();
                byte[] imagen = null;
                publicacion.Titulo = this.Request.Form.titulo;
                publicacion.Descripcion = this.Request.Form.descripcion;
                publicacion.IdCatCategorias = this.Request.Form.idCatCategorias;
                publicacion.IdCatUsuarios = this.Request.Form.idCatUsuarios;
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
                    publicacion.Imagen = imagen == null ? "" : Convert.ToBase64String(imagen, 0, imagen.Length);
                }


                var r = _DA.PostPublicaciones(publicacion);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}