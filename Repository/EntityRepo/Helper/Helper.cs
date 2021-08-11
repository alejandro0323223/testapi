using Entities.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityRepo.Helper
{
    public class Helper<E> : IDisposable, IHelper<E> where E : class
    {
        //protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly RemateEnLinea _ctx;

        public Helper()
        {
            _ctx = new RemateEnLinea();
        }

        public E AgregarEntidad(E entidad, string user, object request)
        {

            try
            {
                if (entidad != null)
                {
                    var dataSet = _ctx.Set<E>();
                    if (entidad is IEnumerable)
                    {
                        dataSet.AddRange(entidad);
                    }
                    else
                    {
                        dataSet.Add(entidad);
                    }
                    //saveBitacora(entidad, user, 1, request);
                    _ctx.SaveChanges();
                }
                _ctx.Database.BeginTransaction().Commit();
                return entidad;
            }
            catch (Exception e)
            {
                _ctx.Database.BeginTransaction().Rollback();
                return null;
            }

        }

        public E EliminaEntidadUsuario(E entidad,int  id_usuario)
        {

            try
            {
                if (entidad != null)
                {
                    var dataSet = _ctx.Set<E>();
                    if (entidad is IEnumerable)
                    {
                        dataSet.RemoveRange(entidad);
                    }
                    else
                    {
                        dataSet.Remove(entidad);
                    }
                    _ctx.SaveChanges();
                }
                _ctx.Database.BeginTransaction().Commit();
                return entidad;
            }
            catch (Exception e)
            {
                _ctx.Database.BeginTransaction().Rollback();
                return null;
            }

        }
        public E FindByValorPK(object dato)
        {
            try
            {
                E auxE;
                auxE = _ctx.Set<E>().Find(dato);
                return auxE;
            }
            catch (Exception e)
            {
                //log.Error("Clase Helper, Metodo FindByValorPK", e);
                return null;
            }
        }

        public List<E> selectAll()
        {
            var dataSet = _ctx.Set<E>();
            return dataSet.ToList();

        }

        public List<E> selectByPredicado(ExpressionStarter<E> pre, string order, int field, int pag, int size)
        {
            List<E> datos = new List<E>();

            var dataSet = _ctx.Set<E>();
            if (String.IsNullOrEmpty(order))
            {
                datos = dataSet.Where(pre).ToList();
            }
            else
            {
                if (!String.IsNullOrEmpty(order))
                {
                    if (!order.Equals(""))
                    {
                        var files = 0;
                        if (pag > 0)
                        {
                            files = (pag - 1) * size;
                        }

                        if (order.Equals("ASC"))
                        {

                            datos = dataSet
                                .Where(pre)
                                .OrderBy(E => field)
                                .Skip(files)
                                .Take(size)
                                .ToList();
                        }
                        else
                        {
                            datos = dataSet
                                .Where(pre)
                                .OrderByDescending(E => field)
                                .Skip(files)
                                .Take(size)
                                .ToList();
                        }

                    }
                    else
                    {
                        if (order.Equals("ASC"))
                        {
                            datos = dataSet
                              .Where(pre)
                              .OrderBy(E => field)
                              .ToList();
                        }
                        else
                        {
                            datos = dataSet
                              .Where(pre)
                              .OrderByDescending(E => field)
                              .ToList();
                        }
                    }
                }
            }
            return datos;
        }
        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public int countByPredicado(ExpressionStarter<E> pre)
        {
            var dataSet = _ctx.Set<E>();
            int count = dataSet.Where(pre).Count();
            return count;
        }

        public E findByPredicado(ExpressionStarter<E> pre)
        {
            var dataSet = _ctx.Set<E>();
            E datos = dataSet.Where(pre).FirstOrDefault();
            return datos; ;
        }

        public List<E> findAllByPredicado(ExpressionStarter<E> pre)
        {
            var dataSet = _ctx.Set<E>();
            List<E> datos = dataSet.Where(pre).ToList();
            return datos; ;
        }


        public E EditarEntidad(E entidad, string user, object request)
        {


            try
            {
                if (entidad != null)
                {
                    var dataSet = _ctx.Set<E>();
                    if (entidad is IEnumerable)
                    {
                        dataSet.UpdateRange(entidad);
                    }
                    else
                    {
                        dataSet.Update(entidad);
                    }
                    //saveBitacora(entidad, user, 2, request);
                    _ctx.SaveChanges();
                }
                _ctx.Database.BeginTransaction().Commit();
                return entidad;
            }
            catch (Exception e)
            {
                _ctx.Database.BeginTransaction().Rollback();
                return null;
            }

        }

    }
}

