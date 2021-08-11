using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public partial class tg_personas
    {
        public tg_personas()
        {
          
            ca_usuarios = new HashSet<ca_usuarios>();
           
        }

        [Key]
        public int id_persona { get; set; }
        [StringLength(500)]
        public string pe_nombrecompleto { get; set; }
        [StringLength(70)]
        public string pe_nombres { get; set; }
        [StringLength(25)]
        public string pe_appaterno { get; set; }
        [StringLength(20)]
        public string pe_apmaterno { get; set; }
        public int? id_tiporut { get; set; }
        [StringLength(10)]
        public string pe_rut { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? pe_fechaingreso { get; set; }
        [StringLength(20)]
        public string pe_nacionalidad { get; set; }
        [StringLength(50)]
        public string pe_giro { get; set; }
        public bool? pe_flgsinrut { get; set; }
        [StringLength(30)]
        public string pe_numctacte { get; set; }
        public int? id_estadocivil { get; set; }
        [StringLength(20)]
        public string pe_telefono { get; set; }
        [StringLength(20)]
        public string pe_celular { get; set; }
        public int? id_banco { get; set; }
        [StringLength(50)]
        public string pe_comuna { get; set; }
        [StringLength(50)]
        public string pe_ciudad { get; set; }
        [StringLength(50)]
        public string pe_pais { get; set; }
        [StringLength(100)]
        public string pe_email { get; set; }
        [StringLength(250)]
        public string pe_direccion { get; set; }
        public int? id_pais { get; set; }
        public int? id_comuna { get; set; }
        public int? id_ciudad { get; set; }
        public int? id_giro { get; set; }
        public int? id_tipocliente { get; set; }
        public int? id_sexo { get; set; }
        public bool? pe_syncsat { get; set; }

     
        public virtual ICollection<ca_usuarios> ca_usuarios { get; set; }
     
       
    }
}
