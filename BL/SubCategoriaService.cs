using DL;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class SubcategoriaService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var subcategorias = context.Subcategoria
                        .Select(s => new
                        {
                            s.IdSubcategoria,
                            s.Nombre,
                            s.Descripcion,
                            s.IdCategoria,
                            IdCategoriaNavigation = new
                            {
                                Nombre = s.IdCategoriaNavigation != null ? s.IdCategoriaNavigation.Nombre : null
                            }
                         }).ToList();
                    result.Results = subcategorias.Cast<object>().ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static DL.Result GetById(int idSubcategoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var subcategoria = context.Subcategoria
                         .Select(s => new
                         {
                             s.IdSubcategoria,
                             s.Nombre,
                             s.Descripcion,
                             s.IdCategoria,
                             IdCategoriaNavigation = new
                             {
                                 Nombre = s.IdCategoriaNavigation != null ? s.IdCategoriaNavigation.Nombre : null
                             }
                         }).FirstOrDefault(sc => sc.IdSubcategoria == idSubcategoria);
                    if (subcategoria != null)
                    {
                        result.Object = subcategoria;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Subcategoría no encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static DL.Result Insert(DL.Subcategorium subcategoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    context.Subcategoria.Add(subcategoria);
                    context.SaveChanges();
                    result.Object = subcategoria;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static DL.Result Update(int id, DL.Subcategorium subcategoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var subcategoriaToUpdate = context.Subcategoria.FirstOrDefault(sc => sc.IdSubcategoria == id);
                    if (subcategoriaToUpdate != null)
                    {
                        subcategoriaToUpdate.Nombre = subcategoria.Nombre;
                        subcategoriaToUpdate.Descripcion = subcategoria.Descripcion;
                        subcategoriaToUpdate.IdCategoria = subcategoria.IdCategoria;

                        context.SaveChanges();
                        result.Object = subcategoriaToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Subcategoría no encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static DL.Result Delete(int idSubcategoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var subcategoriaToDelete = context.Subcategoria.FirstOrDefault(sc => sc.IdSubcategoria == idSubcategoria);
                    if (subcategoriaToDelete != null)
                    {
                        context.Subcategoria.Remove(subcategoriaToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Subcategoría no encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
