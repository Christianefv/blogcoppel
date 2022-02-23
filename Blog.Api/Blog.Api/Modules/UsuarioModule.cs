using Blog.Api.DA;
using Blog.Api.Models;
using Blog.Api.Models.Usuario;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace Blog.Api.Modules
{
    public class UsuarioModule : NancyModule
    {
        private readonly DAUsuario _DA = null;
        public UsuarioModule() : base("/usuarios")
        {
            _DA = new DAUsuario();
            Post("/v1", p => AltaUsuario(p));
            Get("/v1/{idCatUsuario}", p => ConsultarUsuario(p));
            Post("/v1/enviar-correo/{correo}", p => EnviarCorreo(p));
            Get("/v1/validar-codigo-correo/{correo}/{codigo}", p => ValidarCodigoCorreo(p));
        }

        private dynamic ValidarCodigoCorreo(dynamic arg)
        {
            string correo = arg.correo;
            string codigo = arg.codigo;
            try
            {
                var result = _DA.ValidarCodigoCorreo(correo, codigo.ToString());
                return Response.AsJson(result, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.BadRequest);
            }
        }

        private dynamic EnviarCorreo(dynamic arg)
        {
            string correo = arg.correo;
            Random r = new Random();
            try
            {
                int codigo = r.Next(1000000, 9999999);
                var result = _DA.GuardarCodigoCorreo(correo, codigo.ToString());
                if (result.Value)
                {
                    WarmPack.Utilities.MailSender email = new WarmPack.Utilities.MailSender(Globales.smtp, Globales.Puerto, Globales.EnableSsl, Globales.Correo, Globales.Pass);
                    email.Send(Globales.Correo, correo, "Validar correo", "Se envía el código para el registro del blog. Código:" + codigo.ToString(), false);
                }
                
                return Response.AsJson(result, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.BadRequest);
            }
        }

        private dynamic AltaUsuario(dynamic arg)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                byte[] imagen = null;
                usuario.Nombre = this.Request.Form.nombre;
                usuario.Usuario = this.Request.Form.usuario;
                usuario.Password = this.Request.Form.password;
                usuario.Correo = this.Request.Form.correo;

                foreach (var file in this.Request.Files)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.Value.CopyTo(ms);
                        imagen = ms.ToArray();
                    }
                }
                
                usuario.Avatar = imagen == null ? "" : Convert.ToBase64String(imagen, 0, imagen.Length);


                var r = _DA.AltaUsuario(usuario);

                return Response.AsJson(r, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.InternalServerError);
            }
        }        

        private dynamic ConsultarUsuario(dynamic arg)
        {
            try
            {
                int idCatUsuario = arg.idCatUsuario;
                var r = _DA.ConsultarUsuario(idCatUsuario);

                return Response.AsJson(r, HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex, HttpStatusCode.BadRequest);
            }
        }
    }
}