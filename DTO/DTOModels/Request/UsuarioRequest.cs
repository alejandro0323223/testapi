using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModels.Request
{
    public class UsuarioRequest
    {
        public string pe_email { get; set; }
        public List<DateTime> pe_fechaingreso { get; set; }
        public string pe_nombrecompleto { get; set; }
        public string pe_rut { get; set; }
        public string us_consuser { get; set; }
        public string us_esvigente { get; set; }
        public Int32 us_intentosfallidos { get; set; }
        public string us_password { get; set; }
        public string pe_nombres { get; set; }
        public string pe_appaterno { get; set; }
        public string pe_apemanterno { get; set; }
        public int pe_tiporut { get; set; }
        public string pe_telefono { get; set; }
        public string comuna { get; set; }
        public string ciudad { get; set; }
        public string direccion { get; set; }
        public int pe_tipo_documento { get; set; }

        public string us_bloqueado { get; set; }
        public int id_usuario { get; set; }
    }
}
