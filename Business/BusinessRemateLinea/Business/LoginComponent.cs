using BusinessRemateLinea.IBusiness;
using DTOModels.DTO;
using Entities.Entities;
using EntityRepo.Component.Repository;
using LinqKit;
using System;
using Utilidades.Seguridad;
using Utilidades.TokenJWT;

namespace BusinessRemateLinea.Business
{
    public class LoginComponent : ILoginComponent
    {

        public RespuestaDTO LoginAdmin(string user, string pass, string secretkey, string issUserToken, string audience, string tiempoExp,string url)
        {

            RespuestaDTO resp = new RespuestaDTO();
            {
                var pre = PredicateBuilder.New<ca_usuarios>();
                pre = pre.And(x => x.us_consuser == user);
                var usuario = new UsuarioRepository().obtenerUsuario(pre);
                if (usuario != null)
                {
                    var passDecryp = new Seguridad().Decrypt(usuario.us_password);
                    var val = BCrypt.Net.BCrypt.Verify(passDecryp, pass);
                    if (val)
                    {
                        //int id_perfil = 47;
                        //ca_usuariosperfiles uper = new caUsuarioPerfilRepository().obtenerPerfilUsuario(usuario.id_usuario, id_perfil);
                        //if (uper != null)
                        //{
                            ConsultarLoginDTO login = new ConsultarLoginDTO();
                            login.nombreCompleto = usuario.pe_nombrecompleto;
                            login.username = user;
                            login.FechaUltimaConexion = DateTime.Now.ToString();
                            login.tokenJwt = TokenGenerator.GenerateTokenJwt(usuario.us_consuser,"Admin Remate Linea", usuario.id_usuario, secretkey, audience, issUserToken, tiempoExp);
                            login.url = url;
                            resp = resp.OK();
                            resp.resultado = login;
                        //}
                        //else
                        //{
                        //    resp = resp.Error500();
                        //    resp.mensaje = "No existe perfil";
                        //}

                    }
                    else
                    {
                        resp = resp.Error500();
                        resp.mensaje = "No existe invalido";
                    }
                }
                else
                {
                    resp = resp.Error500();
                    resp.mensaje = "No existe usuario";
                }

                return resp;
            }
        }
    }
}