using DL;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class DepartamentoService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    var departamentos = context.Departamentos.ToList();
                    result.Results = departamentos.Cast<object>().ToList();
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

        public static DL.Result GetById(int idDepartamento)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    var departamento = context.Departamentos.FirstOrDefault(d => d.IdDepartamento == idDepartamento);
                    if (departamento != null)
                    {
                        result.Object = departamento;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Departamento no encontrado.";
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

        public static DL.Result Insert(DL.Departamento departamento)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    context.Departamentos.Add(departamento);
                    context.SaveChanges();
                    result.Object = departamento;
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

        public static DL.Result Update(int id, DL.Departamento departamento)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    var departamentoToUpdate = context.Departamentos.FirstOrDefault(d => d.IdDepartamento == id);
                    if (departamentoToUpdate != null)
                    {
                        departamentoToUpdate.Nombre = departamento.Nombre;
                        context.SaveChanges();
                        result.Object = departamentoToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Departamento no encontrado.";
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

        public static DL.Result Delete(int idDepartamento)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    var departamentoToDelete = context.Departamentos.FirstOrDefault(d => d.IdDepartamento == idDepartamento);
                    if (departamentoToDelete != null)
                    {
                        context.Departamentos.Remove(departamentoToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Departamento no encontrado.";
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

        public static DL.Result GetMenuItems()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var menuItems = context.Departamentos
                        .Select(depto => new
                        {
                            DepartamentoId = depto.IdDepartamento,
                            DepartamentoNombre = depto.Nombre,
                            Productos = depto.Productos
                                .Where(p => p.IdCategoria == null) // Solo productos directamente bajo el departamento sin categoría
                                .Select(p => new
                                {
                                    ProductoId = p.IdProductos,
                                    Nombre = p.Nombre
                                }).ToList(),
                            Categorias = depto.Categoria.Select(cat => new
                            {
                                CategoriaId = cat.IdCategoria,
                                CategoriaNombre = cat.Nombre,
                                Productos = cat.Productos
                                    .Where(p => p.IdSubcategoria == null) // Solo productos bajo la categoría que no tienen subcategoría
                                    .Select(p => new
                                    {
                                        ProductoId = p.IdProductos,
                                        Nombre = p.Nombre
                                    }).ToList(),
                                Subcategorias = cat.Subcategoria.Select(sub => new
                                {
                                    SubcategoriaId = sub.IdSubcategoria,
                                    SubcategoriaNombre = sub.Nombre,
                                    Productos = sub.Productos.Select(p => new // Todos los productos bajo la subcategoría
                                    {
                                        ProductoId = p.IdProductos,
                                        Nombre = p.Nombre
                                    }).ToList()
                                })
                            })
                        }).ToList();

                    result.Results = menuItems.Cast<object>().ToList();
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

    }
}
