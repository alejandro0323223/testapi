using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModels.DTO
{
    public class UsuarioDTO
    {
        //datos de ca_usuario
        public int id_usuario { get; set; }
        public int? id_persona { get; set; }
        public string us_consuser { get; set; }
        public string us_password { get; set; }
        public DateTime? us_ultimoacceso { get; set; }
        public int? us_intentosfallidos { get; set; }
        public bool? us_esvigente { get; set; }
        //datos tg persona
        public string pe_nombrecompleto { get; set; }
        public string pe_nombres { get; set; }
        public string pe_appaterno { get; set; }
        public string pe_apmaterno { get; set; }
        public string pe_rut { get; set; }
        public DateTime? pe_fechaingreso { get; set; }
        public string pe_telefono { get; set; }
        public string pe_email { get; set; }
        public string pe_direccion { get; set; }
        //datos tg_persona1 ¬¬
        public string pe1_banco { get; set; }
        public string pe1_cuentaCorriente { get; set; }

        public bool us_bloqueado { get; set; }
        public UsuarioDTO()
        {

        }

        public UsuarioDTO(int id_usuario, int? id_persona, string us_consuser, string us_password, DateTime? us_ultimoacceso, int? us_intentosfallidos, bool? es_vigente, string nombrecomp, string pe_nombres,
            string ap_paterno, string ap_materno, string rut, DateTime? fecha_ingreso, string telefono, string email,string direccion,string banco,string cuenta,bool usbloqueado)
        {
            this.id_persona = id_persona;
            this.id_usuario = id_usuario;
            this.us_consuser = us_consuser;
            this.us_password = us_password;
            this.us_ultimoacceso = us_ultimoacceso;
            this.us_intentosfallidos = us_intentosfallidos;
            this.us_esvigente = es_vigente;
            this.pe_nombrecompleto = nombrecomp;
            this.pe_nombres = pe_nombres;
            this.pe_apmaterno = ap_materno;
            this.pe_appaterno = ap_paterno;
            this.pe_rut = rut;
            this.pe_fechaingreso = fecha_ingreso;
            this.pe_telefono = telefono;
            this.pe_email = email;
            this.pe_direccion = direccion;
            this.pe1_banco = banco;
            this.pe1_cuentaCorriente = cuenta;
            this.us_bloqueado = usbloqueado;
        }



    }
}
