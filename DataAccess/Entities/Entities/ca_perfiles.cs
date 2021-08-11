using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public partial class ca_perfiles
    {
        public ca_perfiles()
        {
            ca_perfilesopciones = new HashSet<ca_perfilesopciones>();
          
            
        }

        public int id_perfil { get; set; }
        [StringLength(50)]
        public string pe_perfil { get; set; }
        public bool? pe_esvigente { get; set; }
        public int? id_sistema { get; set; }
        [StringLength(500)]
        public string pe_descripcion { get; set; }
        public int? pe_orden { get; set; }

        [ForeignKey("id_sistema")]
        [InverseProperty("ca_perfiles")]
       
      
        public virtual ICollection<ca_perfilesopciones> ca_perfilesopciones { get; set; }
   
        
     
    }
}
