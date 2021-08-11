using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModels.DTO
{
    public class RespuestaDTO
    {
        public object resultado { get; set; }
        public string codigo { get; set; }
        public int numero { get; set; }
        public string mensaje { get; set; }
        public RespuestaDTO()
        {

        }

        public  RespuestaDTO
            
            Error500()
        {
            this.codigo = "ERROR";
            this.numero = 500;
            this.mensaje = "No se pudo realizar la accion";
            return this;
        }

        public RespuestaDTO OK()
        {
            this.codigo = "OK";
            this.numero = 200;
            this.mensaje = "";
            return this;
        }
    }
}
