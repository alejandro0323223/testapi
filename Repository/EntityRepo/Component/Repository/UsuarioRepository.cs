using DTOModels.DTO;
using DTOModels.ModeloQuery;
using DTOModels.Request;
using Entities.Entities;
using EntityRepo.Component.IRepository;
using EntityRepo.Helper;
using LinqKit;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace EntityRepo.Component.Repository
{
    public class UsuarioRepository : Helper<ca_usuarios>, IUsuarioRepository
    {


        public Paginacion<UsuarioRequest> obtenerListaUsuarios(Paginacion<UsuarioRequest> query, ExpressionStarter<ca_usuarios> pre)
        {
            List<UsuarioDTO> retorno = new List<UsuarioDTO>();
            var datos = this.selectByPredicado(pre, query.order, query.field, query.pagina, query.cantidad);
            if (query.total == 0)
            {
                query.total = this.countByPredicado(pre);
            }
            retorno = datos
                  .Select(x =>
                        caUsuarioToUsuarioDTO(x,true)
                     )
                  .ToList();
            query.resultado = retorno;
            return query;
        }

        public bool Editar(ca_usuarios usuario,string user,object request)
        {
            using (var ctx = new RemateEnLinea())
            {
                using(var dbT = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        this.EditarEntidad(usuario, user, request);
                        dbT.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        dbT.Rollback();
                        return false;
                    }
                }
            }
        }

        public int validacionEmail(string email)
        {
            using (var ctx = new RemateEnLinea())
            {
                return ctx.tg_personas.Where(x => x.pe_email.Contains(email)).Count();
            }
        }
        public int validacionRut(string rut)
        {
            using (var ctx = new RemateEnLinea())
            {
                return ctx.tg_personas.Where(x => x.pe_rut.Contains(rut)).Count();
            }
        }
        public int validacionNombreUsuario(string username)
        {
            using (var ctx = new RemateEnLinea())
            {
                return ctx.ca_usuarios.Where(x => x.us_consuser.Contains(username)).Count();
            }
        }
        public UsuarioDTO obtenerUsuario(ExpressionStarter<ca_usuarios> pre)
        {
            using (var ctx = new RemateEnLinea())
            {
                var usu = ctx.ca_usuarios.Where(pre).FirstOrDefault();
                return caUsuarioToUsuarioDTO(usu, false);
            }
            
            
        }
        public UsuarioDTO caUsuarioToUsuarioDTO(ca_usuarios x,bool nopass)
        {
            if (nopass)
            {
                x.us_password = " ";
            }
            return new UsuarioDTO(
                      x.id_usuario,
                      x.id_persona,
                      x.us_consuser,
                      x.us_password,
                      x.us_ultimoacceso,
                      x.us_intentosfallidos,
                      x.us_esvigente,
                      x.id_personaNavigation.pe_nombrecompleto,
                      x.id_personaNavigation.pe_nombres,
                      x.id_personaNavigation.pe_apmaterno,
                      x.id_personaNavigation.pe_appaterno,
                      x.id_personaNavigation.pe_rut,
                      x.id_personaNavigation.pe_fechaingreso,
                      x.id_personaNavigation.pe_telefono,
                      x.id_personaNavigation.pe_email,
                      x.id_personaNavigation.pe_direccion,
                      "",
                      "",
                      x.us_bloqueado??false
                  //this.getDatosBanco(1, x.id_persona),
                  //this.getDatosBanco(2, x.id_persona)
                  );
        }
        private string getDatosBanco(int opcion, int? id_persona)
        {
            try
            {
                string dato = "";
                //switch (opcion)
                //{
                //    case 1:
                //        dato = _ctx.tg_personas_1.Where(w => w.id_persona == id_persona).FirstOrDefault().pe1_banco;
                //        break;
                //    case 2:
                //        dato = _ctx.tg_personas_1.Where(w => w.id_persona == id_persona).FirstOrDefault().pe1_cuentaCorriente;
                //        break;
                //    default:
                //        dato = "";
                //        break;

                //}
                return dato;
            }
            catch
            {
                return "";
            }
        }
    }
}
