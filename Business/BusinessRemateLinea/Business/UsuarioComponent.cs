using BusinessRemateLinea.IBusiness;
using DTOModels.DTO;
using DTOModels.ModeloQuery;
using DTOModels.Request;
using Entities.Entities;
using EntityRepo.Component.Repository;
using LinqKit;
using System;
using Utilidades.Seguridad;

namespace BusinessRemateLinea.Business
{
    public class UsuarioComponent : IUsuarioComponent
    {
        public Paginacion<UsuarioRequest> obtenerListaUsuarios(Paginacion<UsuarioRequest> query)
        {
            var pre = PredicateBuilder.New<ca_usuarios>();
            pre = pre.And(x => x.id_persona > -1);
            if (query.consulta.pe_rut != "")
            {
                pre = pre.And(x => x.id_personaNavigation.pe_rut.Contains(query.consulta.pe_rut));
            }
            if (query.consulta.pe_nombrecompleto != "")
            {
                pre = pre.And(x => x.id_personaNavigation.pe_nombrecompleto.Contains(query.consulta.pe_nombrecompleto));
            }
            if (query.consulta.pe_email != "")
            {
                pre = pre.And(x => x.id_personaNavigation.pe_email.Contains(query.consulta.pe_email));
            }
            if (query.consulta.us_consuser != "")
            {
                pre = pre.And(x => x.us_consuser.Contains(query.consulta.us_consuser));
            }
            if (query.consulta.us_intentosfallidos > 0)
            {
                pre = pre.And(x => x.us_intentosfallidos > query.consulta.us_intentosfallidos);
            }
            if (query.consulta.pe_fechaingreso.Count > 0)
            {
                if (query.consulta.pe_fechaingreso.Count == 1)
                {
                    pre = pre.And(x => x.id_personaNavigation.pe_fechaingreso >= query.consulta.pe_fechaingreso[0]);
                }
                else
                {
                    pre = pre.And(x => x.id_personaNavigation.pe_fechaingreso >= query.consulta.pe_fechaingreso[0] && x.id_personaNavigation.pe_fechaingreso <= query.consulta.pe_fechaingreso[1]);
                }
            }
            if (query.consulta.us_esvigente != "NO")
            {
                if (query.consulta.us_esvigente == "true")
                {
                    pre = pre.And(x => x.us_esvigente == true);
                }
                else
                {
                    pre = pre.And(x => x.us_esvigente == false);
                }
            }
            if (query.consulta.us_bloqueado != "NO")
            {
                if (query.consulta.us_bloqueado == "true")
                {
                    pre = pre.And(x => x.us_bloqueado == true);
                }
                else
                {
                    pre = pre.And(x => x.us_bloqueado == false);
                }
            }
            try
            {
                query = new UsuarioRepository().obtenerListaUsuarios(query, pre);
                query.codigo = "OK";
                query.numero = 200;
                return query;
            }
            catch (Exception e)
            {
                query.codigo = "ERROR";
                query.numero = 500;
                query.mensaje = e.Message;
                return query;
            }

        }
        public RespuestaDTO desactivarUsuario(UsuarioRequest user, string username)
        {
            RespuestaDTO resp = new RespuestaDTO();
            var userRepo = new UsuarioRepository();
            var pre = PredicateBuilder.New<ca_usuarios>();
            pre = pre.And(x => x.us_consuser == user.us_consuser);
            try
            {
                ca_usuarios usubd = userRepo.findByPredicado(pre);
                if (usubd != null)
                {
                    bool aux = false;
                    if (user.us_esvigente.Equals("true"))
                    {
                        aux = true;
                    }
                    usubd.us_esvigente = aux;
                    var result = userRepo.EditarEntidad(usubd, username, user);
                    if (result != null)
                    {
                        resp.codigo = "OK";
                        resp.numero = 200;
                        usubd = userRepo.findByPredicado(pre);
                        resp.resultado = userRepo.caUsuarioToUsuarioDTO(usubd,true);
                        if (aux)
                        {
                            resp.mensaje = "Se ha activado el usuario";
                        }
                        else
                        {
                            resp.mensaje = "Se ha desactivado el usuario";
                        }
                    }
                    else
                    {
                        resp.codigo = "ERROR";
                        resp.mensaje = "No se ha modificado el usuario";
                        resp.numero = 500;
                        resp.resultado = "";
                    }
                }
                else
                {
                    resp.codigo = "ERROR";
                    resp.mensaje = "No se ha encontrado datos de usuario en bd";
                    resp.numero = 500;
                    resp.resultado = "";
                }
            }
            catch (Exception e)
            {
                resp.codigo = "ERROR";
                resp.mensaje = e.Message;
                resp.numero = 500;
                resp.resultado = e;
            }
            return resp;
        }
        public RespuestaDTO cambiarContrasena(UsuarioRequest user, string username)
        {
            RespuestaDTO resp = new RespuestaDTO();
            using (var userRepo = new UsuarioRepository())
            {
                try
                {
                    var pre = PredicateBuilder.New<ca_usuarios>();
                    pre = pre.And(x => x.us_consuser == user.us_consuser);
                    ca_usuarios usubd = userRepo.findByPredicado(pre);
                    if (usubd.us_esvigente == true)
                    {
                        var passold = new Seguridad().Decrypt(usubd.us_password);
                        if (!passold.Contains(user.us_password))
                        {
                            usubd.us_password = new Seguridad().Encrypt(user.us_password);
                            var aux = userRepo.EditarEntidad(usubd, username, user);
                            if (aux != null)
                            {
                                resp.codigo = "OK";
                                resp.numero = 200;
                                resp.mensaje = "Se ha cambiado la contraseña";
                            }
                            else
                            {
                                resp = resp.Error500();
                                resp.resultado = "No se pudo cambiar la contraseña";
                            }
                        }
                        else
                        {
                            resp = resp.Error500();
                            resp.resultado = "La contraseña no pudo cambiarse, porque es igual a la anterior.";
                        }
                    }
                    else
                    {
                        resp = resp.Error500();
                        resp.resultado = "No se pudo cambiar la contraseña usuario no vigente";
                    }

                }
                catch (Exception e)
                {
                    resp = resp.Error500();
                    resp.resultado = "No se pudo cambiar la contraseña";
                }
            }


            return resp;
        }
        public RespuestaDTO obtenerUsuarioRequest(string us_conuser)
        {
            RespuestaDTO resp = new RespuestaDTO();
            UsuarioRepository userRepo = new UsuarioRepository();
            var pre = PredicateBuilder.New<ca_usuarios>();
            pre = pre.And(x => x.us_consuser == us_conuser);
            ca_usuarios usubd = userRepo.findByPredicado(pre);
            if (usubd != null)
            {
                UsuarioRequest ur = new UsuarioRequest();
                ur.pe_nombrecompleto = usubd.id_personaNavigation.pe_nombrecompleto;
                ur.pe_rut = usubd.id_personaNavigation.pe_rut;
                ur.us_consuser = usubd.us_consuser;
                ur.pe_email = usubd.id_personaNavigation.pe_email;
                ur.us_esvigente = usubd.us_esvigente.ToString();
                ur.us_intentosfallidos = usubd.us_intentosfallidos ?? 0;
                ur.us_password = "";
                ur.pe_nombres = usubd.id_personaNavigation.pe_nombres;
                ur.pe_appaterno = usubd.id_personaNavigation.pe_appaterno;
                ur.pe_apemanterno = usubd.id_personaNavigation.pe_apmaterno;
                ur.pe_tiporut = usubd.id_personaNavigation.id_tiporut ?? 1;
                ur.pe_telefono = usubd.id_personaNavigation.pe_telefono;
                ur.comuna = ""+usubd.id_personaNavigation.id_comuna;
                ur.ciudad = ""+usubd.id_personaNavigation.id_ciudad;
                ur.direccion = usubd.id_personaNavigation.pe_direccion;
                ur.pe_tipo_documento = 0;
                ur.id_usuario = usubd.id_usuario;
                resp = resp.OK();
                resp.resultado = ur;
            }
            else
            {
                resp = resp.Error500();
                resp.mensaje = "Usuario no encontrado";
            }
            return resp;
        }
        /// <summary>
        /// Actualización de usuario por entidad
        /// </summary>
        /// <param name="UsuarioRequest"></param>
        /// <returns></returns>
        public RespuestaDTO actualizarUsuario(UsuarioRequest usuario)
        {
                RespuestaDTO resp = new RespuestaDTO();
                using (var userRepo = new UsuarioRepository())
                {
                    try
                    {
                        var pre = PredicateBuilder.New<ca_usuarios>();
                        pre = pre.And(x => x.id_usuario == usuario.id_usuario);
                        ca_usuarios usubd = userRepo.findByPredicado(pre);
                        if (usubd.us_esvigente == true)
                        {
                                var aux = userRepo.EditarEntidad(usubd, usubd.id_usuario.ToString(), usuario.id_usuario);
                                if (aux != null)
                                {
                                    resp.codigo = "OK";
                                    resp.numero = 200;
                                    resp.mensaje = "Se ha actualizado el usuario";
                                }
                                else
                                {
                                    resp = resp.Error500();
                                    resp.resultado = "No se pudo actualizar el usuario";
                                }
                            }
                    }
                    catch (Exception e)
                    {
                        resp = resp.Error500();
                        resp.resultado = "No se pudo cambiar la contraseña";
                    }
                }
                return resp;
            }
         /// <summary>
         ///    Elminación de Usuario por id del usuario
         /// </summary>
         /// <param name="id_usuario"></param>
         /// <returns></returns>
        public RespuestaDTO eliminarUsuario(int id_usuario)
        {
            RespuestaDTO resp = new RespuestaDTO();
            using (var userRepo = new UsuarioRepository())
            {
                try
                {
                    var pre = PredicateBuilder.New<ca_usuarios>();
                    pre = pre.And(x => x.id_usuario == id_usuario);
                    ca_usuarios usubd = userRepo.findByPredicado(pre);
                    if (usubd.us_esvigente == true)
                    {
                        var aux = userRepo.EliminaEntidadUsuario(usubd, usubd.id_usuario);
                        if (aux != null)
                        {
                            resp.codigo = "OK";
                            resp.numero = 200;
                            resp.mensaje = "Se ha eliminado el usuario";
                        }
                        else
                        {
                            resp = resp.Error500();
                            resp.resultado = "No se pudo eliminar el usuario";
                        }
                    }



                }
                catch (Exception e)
                {
                    resp = resp.Error500();
                    resp.resultado = "No se pudo cambiar la contraseña";
                }
            }
            return resp;


        }

    }
}
