using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class CategoriaService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                     var categorias = context.Categoria
                        .Select(c => new 
                        {
                            c.IdCategoria,
                            c.Nombre,
                            c.Descripcion,
                            c.IdDepartamento,
                            IdDepartamentoNavigation = new
                            {
                                Nombre = c.IdDepartamentoNavigation != null ? c.IdDepartamentoNavigation.Nombre : null
                            }
                        })
                        .ToList();
                    result.Results = categorias.Cast<object>().ToList();
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

        public static DL.Result GetById(int idCategoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var categoria = context.Categoria
                        .Select(c => new
                        {
                            c.IdCategoria,
                            c.Nombre,
                            c.Descripcion,
                            c.IdDepartamento,
                            IdDepartamentoNavigation = new
                            {
                                Nombre = c.IdDepartamentoNavigation != null ? c.IdDepartamentoNavigation.Nombre : null
                            }
                        })
                        .FirstOrDefault(c => c.IdCategoria == idCategoria);
                    if (categoria != null)
                    {
                        result.Object = categoria;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Categoría no encontrada.";
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

        public static DL.Result Insert(DL.Categorium categoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    context.Categoria.Add(categoria);
                    context.SaveChanges();
                    result.Object = categoria;
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

        public static DL.Result Update(int id, DL.Categorium categoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var categoriaToUpdate = context.Categoria.FirstOrDefault(c => c.IdCategoria == id);
                    if (categoriaToUpdate != null)
                    {
                        categoriaToUpdate.Nombre = categoria.Nombre;
                        categoriaToUpdate.Descripcion = categoria.Descripcion;
                        categoriaToUpdate.IdDepartamento = categoria.IdDepartamento;

                        context.SaveChanges();
                        result.Object = categoriaToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Categoría no encontrada.";
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

        public static DL.Result Delete(int idCategoria)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var categoriaToDelete = context.Categoria.FirstOrDefault(c => c.IdCategoria == idCategoria);
                    if (categoriaToDelete != null)
                    {
                        context.Categoria.Remove(categoriaToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Categoría no encontrada.";
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
