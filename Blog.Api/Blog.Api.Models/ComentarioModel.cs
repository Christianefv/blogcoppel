using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api.Models
{
    public class ComentarioModel
    {
        public int IdComentarios { get; set; }
        public int IdCatUsuarios { get; set; }
        public int IdCatPublicaciones { get; set; }
        public string Comentarios { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Avatar { get; set; }
    }
}
