using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModels.Request
{
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }

        public string Url { get; set; }
    }
}
