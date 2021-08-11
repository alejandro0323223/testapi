using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public partial class ca_usuarios
    {
        public ca_usuarios()
        {

            ca_usuarioadministrador = new HashSet<ca_usuarioadministrador>();

        }

        public int id_usuario { get; set; }
        public int? id_persona { get; set; }
        public bool? us_esvigente { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? us_ultimaconexion { get; set; }
        [StringLength(100)]
        public string us_consuser { get; set; }
        [StringLength(200)]
        public string us_password { get; set; }
        public bool? us_cambioPassword { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? us_ultimoacceso { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? us_feccambiopass { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? us_fechaexpiracion { get; set; }
        public int? us_intentosfallidos { get; set; }
        public bool? us_correo_verificado { get; set; }
        public bool? us_bloqueado { get; set; }

        [ForeignKey("id_persona")]
        [InverseProperty("ca_usuarios")]
        public virtual tg_personas id_personaNavigation { get; set; }

        [InverseProperty("id_usuarioNavigation")]
        public virtual ICollection<ca_usuarioadministrador> ca_usuarioadministrador { get; set; }
     

     
      

    }
}
