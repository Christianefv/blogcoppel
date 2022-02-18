using Blog.Api.DA;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Api.Modules
{
    public class InicioModule : NancyModule
    {
        private readonly DACategoria _DA = null;
        public InicioModule() : base("/inicio")
        {
            _DA = new DACategoria();
            Get("/categorias/v1/{idCatCategorias}", p => GetCategorias(p));
        }

        private dynamic GetCategorias(dynamic arg)
        {
            try
            {
                int idCatCategorias = arg.idCatCategorias;
                var r = _DA.GetCategoria(idCatCategorias);

                return Response.AsJson(r, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.BadRequest);
            }
        }
    }
}