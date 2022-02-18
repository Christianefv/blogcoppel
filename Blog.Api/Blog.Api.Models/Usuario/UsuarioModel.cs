using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api.Models.Usuario
{
    public class UsuarioModel
    {
        public int IdCatUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public bool Administrador { get; set; }
    }
}
