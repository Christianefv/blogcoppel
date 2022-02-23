using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Api.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get("/", _ => GetRoot());
        }

        private object GetRoot()
        {
            string Mesanje = "la Api se encuentra funcionando 2022/02/22.4";

            return Response.AsJson(Mesanje);
        }
    }
}