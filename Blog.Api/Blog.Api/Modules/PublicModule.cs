using Blog.Api.DA;
using Blog.Api.Models;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Api.Modules
{
    public class PublicModule : NancyModule
    {
        private readonly DACategoria _DA = null;
        private readonly DAPublicaciones _DAPublicaciones = null;
        public PublicModule() : base("/publico")
        {
            
            _DA = new DACategoria();
            _DAPublicaciones = new DAPublicaciones();
            Get("/categorias/v1/{idCatCategorias}", p => ObtenerCategorias(p));
            Get("publicaciones/v1/{idCatUsuarios}/{idCatCategorias}/{idCatPublicaciones}", p => ObtenerPublicaciones(p));
        }

        private dynamic ObtenerPublicaciones(dynamic arg)
        {
            try
            {
                var publicacion = new PublicacionModel();
                publicacion.IdCatPublicaciones = arg.idCatPublicaciones;
                publicacion.IdCatCategorias = arg.idCatCategorias;
                publicacion.IdCatUsuarios = arg.idCatUsuarios;
                var r = _DAPublicaciones.GetPublicaciones(publicacion);

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
                return Response.AsJson(ex, HttpStatusCode.BadRequest);
            }
        }
    }
}