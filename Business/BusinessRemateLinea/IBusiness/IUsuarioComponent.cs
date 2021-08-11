using DTOModels.DTO;
using DTOModels.ModeloQuery;
using DTOModels.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRemateLinea.IBusiness
{
    public interface IUsuarioComponent
    {
        Paginacion<UsuarioRequest> obtenerListaUsuarios(Paginacion<UsuarioRequest> query);
        RespuestaDTO desactivarUsuario(UsuarioRequest user, string username);
        RespuestaDTO cambiarContrasena(UsuarioRequest user, string username);
      

        RespuestaDTO obtenerUsuarioRequest(string us_conuser);
    }
}
