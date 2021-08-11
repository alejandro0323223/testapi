using LinqKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityRepo.Helper
{
    public interface IHelper<E>
    {
        List<E> selectAll();
        E AgregarEntidad(E entidad, string user, object request);
        E EditarEntidad(E entidad, string user, object request);
        E FindByValorPK(Object dato);
        E findByPredicado(ExpressionStarter<E> pre);
        List<E> selectByPredicado(ExpressionStarter<E> pre, string order, int field, int pag, int size);
        int countByPredicado(ExpressionStarter<E> pre);

        List<E> findAllByPredicado(ExpressionStarter<E> pre);
    }
}
