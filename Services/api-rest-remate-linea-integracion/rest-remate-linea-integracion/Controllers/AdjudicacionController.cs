using System;
using System.Collections.Generic;
using BusinessRemateLinea.IBusiness;
using DTOModels.DTO;
using DTOModels.ModeloQuery;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace rest_remate_linea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjudicacionController : ControllerBase
    {

        private readonly IAdjudicacionComponent _adjudicacionComponent;
        public AdjudicacionController(IAdjudicacionComponent adjudicacionComponent)
        {
            _adjudicacionComponent =adjudicacionComponent;
        }

        [Route("MisAdjudicaciones")]
        [HttpPost]
        [SwaggerOperation("MisAdjudicaciones")]
        [SwaggerResponseAttribute(statusCode: 200, type: typeof(Paginacion<AdjudicacionDTO>), description: "Success")]
        public ActionResult<Paginacion<AdjudicacionDTO>> AgregarModificarRemate(Paginacion<AdjudicacionDTO> query)
        {
          
            Paginacion<AdjudicacionDTO> resp = _adjudicacionComponent.listaAdjudicaciones(query);
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
