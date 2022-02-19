using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api.Models
{
    public class Globales
    {
        public static string ConexionPrincipal => "data source=desarrolloblog.database.windows.net; initial catalog = blog; user id = administrador; password = Qwerty1234";
        public static string Correo = "correoblogcf@gmail.com";
        public static string Pass = "Qwerty1234.";
        public static int Puerto = 587;
        public static string smtp = "smtp.gmail.com";
        public static bool EnableSsl = true;
        public static string UrlApiBlog = "https://blogapi20220129.azurewebsites.net/";
    }
}
