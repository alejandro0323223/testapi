using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public partial class ca_usuarioadministrador
    {
        [Key]
        public int id_usuarioadm { get; set; }
        [StringLength(50)]
        public string usa_descripcion { get; set; }
        public int? id_usuario { get; set; }

        [ForeignKey("id_usuario")]
        [InverseProperty("ca_usuarioadministrador")]
        public virtual ca_usuarios id_usuarioNavigation { get; set; }
    }
}
