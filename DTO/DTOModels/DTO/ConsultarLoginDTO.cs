
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModels.DTO
{
    public class ConsultarLoginDTO
    {
        public string username { get; set; }
        public string nombreCompleto { get; set; }
        public string FechaUltimaConexion { get; set; }
        public string rolName { get; set; }
        public int? id_mandante { get; set; }
        public int? id_persona { get; set; }
        public int? id_usuario { get; set; }
        public List<string> permisos { get; set; }
        public string tokenJwt { get; set; }
   
        public string avatar { get; set; }
        public string url { get; set; }
    }
}
