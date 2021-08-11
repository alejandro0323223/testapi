using DTOModels.DTO;
using DTOModels.ModeloQuery;
using DTOModels.Request;
using Entities.Entities;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityRepo.Component.IRepository
{
    public interface IUsuarioRepository
    {
        Paginacion<UsuarioRequest> obtenerListaUsuarios(Paginacion<UsuarioRequest> query, ExpressionStarter<ca_usuarios> pre);

    

        UsuarioDTO obtenerUsuario(ExpressionStarter<ca_usuarios> pre);

        UsuarioDTO caUsuarioToUsuarioDTO(ca_usuarios x, bool nopass);

        int validacionEmail(string email);

        int validacionRut(string rut);

        int validacionNombreUsuario(string username);


       
    }
}
