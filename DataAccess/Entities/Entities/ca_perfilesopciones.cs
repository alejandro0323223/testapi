using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public partial class ca_perfilesopciones
    {
        public int id_perfilopcion { get; set; }
        public int? id_perfil { get; set; }
        public int? id_opcion { get; set; }

     
 
     
        [InverseProperty("ca_perfilesopciones")]
        public virtual ca_perfiles id_perfilNavigation { get; set; }
    }
}
