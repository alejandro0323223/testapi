using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessRemateLinea.Business;
using BusinessRemateLinea.IBusiness;
using DTOModels.DTO;
using DTOModels.ModeloQuery;
using DTOModels.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace rest_remate_linea_admin.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
       

       
        [Route("Desactivar")]
        [HttpPost]
        [SwaggerOperation("Desactivar")]
        [SwaggerResponseAttribute(statusCode: 200, type: typeof(RespuestaDTO), description: "Success")]
        public ActionResult<RespuestaDTO> Desactivar(UsuarioRequest query)
        {
            string username = Request.Headers["username"];
            RespuestaDTO resp = new UsuarioComponent().desactivarUsuario(query, username);
            if (resp.codigo == "OK")
            {
                return Ok(resp);
            }
            else
            {
                return StatusCode(500, resp);
            }
        }

        [Route("CambiarContrasena")]
        [HttpPost]
        [SwaggerOperation("CambiarContrasena")]
        [SwaggerResponseAttribute(statusCode: 200, type: typeof(RespuestaDTO), description: "Success")]
        public ActionResult<RespuestaDTO> CambiarContrasena(UsuarioRequest query)
        {
            string username = Request.Headers["username"];
            RespuestaDTO resp = new UsuarioComponent().cambiarContrasena(query, username);
            if (resp.codigo == "OK")
            {
                return Ok(resp);
            }
            else
            {
                return StatusCode(500, resp);
            }
        }

        [Route("CrearModificarUsuario")]
        [HttpPost]
        [SwaggerOperation("CrearModificarUsuario")]
        [SwaggerResponseAttribute(statusCode: 200, type: typeof(RespuestaDTO), description: "Success")]
        public ActionResult<RespuestaDTO> CrearModificarUsuario(UsuarioRequest query)
        {
            string username = Request.Headers["username"];
            RespuestaDTO resp = new UsuarioComponent().actualizarUsuario(query);
            if (resp.codigo == "OK")
            {
                return Ok(resp);
            }
            else
            {
                return StatusCode(500, resp);
            }
        }

        [Route("EliminarUsuario")]
        [HttpPost]
        [SwaggerOperation("EliminarUsuario")]
        [SwaggerResponseAttribute(statusCode: 200, type: typeof(RespuestaDTO), description: "Success")]
        public ActionResult<RespuestaDTO> EliminarUsuario(int id_usuario)
        {
            string username = Request.Headers["username"];
            RespuestaDTO resp = new UsuarioComponent().eliminarUsuario(id_usuario);
            if (resp.codigo == "OK")
            {
                return Ok(resp);
            }
            else
            {
                return StatusCode(500, resp);
            }
        }
        [Route("ObtenerUsuarioConUser")]
        [HttpGet]
      
        [SwaggerResponseAttribute(statusCode: 200, type: typeof(RespuestaDTO), description: "Success")]
        public ActionResult<RespuestaDTO> ObtenerUsuarioConUser(string us_conuser)
        {
            string username = Request.Headers["username"];
            RespuestaDTO resp = new UsuarioComponent().obtenerUsuarioRequest(us_conuser);
            if (resp.codigo == "OK")
            {
                return Ok(resp);
            }
            else
            {
                return StatusCode(500, resp);
            }
        }


      
    }
}
