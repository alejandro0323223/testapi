using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModels.ModeloQuery
{
    public class Paginacion<E> where E : class
    {
        public string order { get; set; }
        public int field { get; set; }
        public int pagina { get; set; }
        public int cantidad { get; set; }
        public E consulta { get; set; }
        public object resultado { get; set; }
        public string codigo { get; set; }
        public int numero { get; set; }
        public string mensaje { get; set; }
        public int total { get; set; }

    }
}
