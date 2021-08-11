using DTOModels.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRemateLinea.IBusiness
{
    interface ILoginComponent
    {
        RespuestaDTO LoginAdmin(string user, string pass, string secretkey, string issUserToken, string audience, string tiempoExp,string url);
    }
}
