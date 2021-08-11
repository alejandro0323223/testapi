using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessRemateLinea.Business;
using DTOModels.DTO;
using DTOModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Utilidades.Seguridad;

namespace rest_remate_linea_admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [Route("LoginAdmin")]
        [HttpPost]
        [SwaggerOperation("LoginAdmin")]
        [SwaggerResponseAttribute(statusCode: 200, type: typeof(RespuestaDTO), description: "Success")]
        public ActionResult<RespuestaDTO> LoginAdmin(string token)
        {
           

            //
            IConfigurationBuilder conf = new ConfigurationBuilder();
            conf.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = conf.Build();
            string secretKey = root.GetValue<string>("JWT_SECRET_KEY");
            string issUserToken = root.GetValue<string>("JWT_ISSUER_TOKEN");
            string audience = root.GetValue<string>("JWT_AUDIENCE_TOKEN");
            string expMin = root.GetValue<string>("JWT_EXPIRE_MINUTES");
            string key = root.GetValue<string>("key"); ;
            string iv = root.GetValue<string>("iv"); ;

            string decrypt = new SecurityAES(key, iv).decrypt(token);
            LoginRequest login = JsonConvert.DeserializeObject<LoginRequest>(decrypt);

           RespuestaDTO resp = new LoginComponent().LoginAdmin(login.Usuario,login.Clave,secretKey,issUserToken,audience,expMin,login.Url);
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
