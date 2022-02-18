using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api.Models
{
    public class PublicacionModel
    {
        public int IdCatUsuarios { get; set; }
        public int IdCatPublicaciones { get; set; }
        public int IdCatCategorias { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
    }
}
