using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class ProductoService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (InnotecContext context = new InnotecContext())
                {
                    // Utiliza LINQ para realizar los joins y seleccionar los campos específicos
                    var products = context.Productos
                        .Include(p => p.IdDepartamentoNavigation)
                        .Include(p => p.IdCategoriaNavigation)
                        .Include(p => p.IdSubcategoriaNavigation)
                        .Select(p => new
                        {
                            IdProductos = p.IdProductos,
                            Nombre = p.Nombre,
                            DescripcionDelProducto = p.DescripcionDelProducto,
                            Precio = p.Precio,
                            Cantidad = p.Cantidad,
                            ImagenDelProducto = p.ImagenDelProducto,
                            Departamento = new { Id = p.IdDepartamentoNavigation.IdDepartamento, Nombre = p.IdDepartamentoNavigation.Nombre },
                            Categoria = new { Id = p.IdCategoriaNavigation.IdCategoria, Nombre = p.IdCategoriaNavigation.Nombre },
                            Subcategoria = new { Id = p.IdSubcategoriaNavigation.IdSubcategoria, Nombre = p.IdSubcategoriaNavigation.Nombre }
                        }).ToList();

                    result.Results = products.Cast<object>().ToList();
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

        public static DL.Result GetByName(string NameProduct)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext(new DbContextOptions<InnotecContext>()))
                {
                    var product = context.Productos
                        .Include(p => p.IdDepartamentoNavigation)
                        .Include(p => p.IdCategoriaNavigation)
                        .Include(p => p.IdSubcategoriaNavigation)
                        .Where(p => p.Nombre.Contains(NameProduct))
                        .Select(p => new
                        {
                            IdProductos = p.IdProductos,
                            Nombre = p.Nombre,
                            DescripcionDelProducto = p.DescripcionDelProducto,
                            Precio = p.Precio,
                            Cantidad = p.Cantidad,
                            ImagenDelProducto = p.ImagenDelProducto,
                            Departamento = new { Id = p.IdDepartamentoNavigation.IdDepartamento, Nombre = p.IdDepartamentoNavigation.Nombre },
                            Categoria = new { Id = p.IdCategoriaNavigation.IdCategoria, Nombre = p.IdCategoriaNavigation.Nombre },
                            Subcategoria = new { Id = p.IdSubcategoriaNavigation.IdSubcategoria, Nombre = p.IdSubcategoriaNavigation.Nombre }
                        }).ToList();

                    if (product != null)
                    {
                        result.Results = product.Cast<object>().ToList();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Producto no encontrado.";
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

        public static DL.Result GetById(int idProducto)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext(new DbContextOptions<InnotecContext>()))
                {
                    var product = context.Productos
                        .Include(p => p.IdDepartamentoNavigation)
                        .Include(p => p.IdCategoriaNavigation)
                        .Include(p => p.IdSubcategoriaNavigation)
                        .Select(p => new
                        {
                            IdProductos = p.IdProductos,
                            Nombre = p.Nombre,
                            DescripcionDelProducto = p.DescripcionDelProducto,
                            Precio = p.Precio,
                            Cantidad = p.Cantidad,
                            ImagenDelProducto = p.ImagenDelProducto,
                            Departamento = new { Id = p.IdDepartamentoNavigation.IdDepartamento, Nombre = p.IdDepartamentoNavigation.Nombre },
                            Categoria = new { Id = p.IdCategoriaNavigation.IdCategoria, Nombre = p.IdCategoriaNavigation.Nombre },
                            Subcategoria = new { Id = p.IdSubcategoriaNavigation.IdSubcategoria, Nombre = p.IdSubcategoriaNavigation.Nombre }
                        }).FirstOrDefault(p => p.IdProductos == idProducto);
                    if (product != null)
                    {
                        result.Object = product;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Producto no encontrado.";
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

        public static DL.Result Insert(DL.Producto producto)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext(new DbContextOptions<InnotecContext>()))
                {
                    context.Productos.Add(producto);
                    context.SaveChanges();
                    result.Object = producto;
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

        public static DL.Result Update(int id, DL.Producto producto)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext(new DbContextOptions<InnotecContext>()))
                {
                    var productoToUpdate = context.Productos.FirstOrDefault(p => p.IdProductos == id);
                    if (productoToUpdate != null)
                    {
                        productoToUpdate.Nombre = producto.Nombre;
                        productoToUpdate.DescripcionDelProducto = producto.DescripcionDelProducto;
                        productoToUpdate.Precio = producto.Precio;
                        productoToUpdate.Cantidad = producto.Cantidad;
                        productoToUpdate.ImagenDelProducto = producto.ImagenDelProducto;
                        productoToUpdate.IdProveedor = producto.IdProveedor;
                        productoToUpdate.IdDepartamento = producto.IdDepartamento;
                        productoToUpdate.IdCategoria = producto.IdCategoria;
                        productoToUpdate.IdSubcategoria = producto.IdSubcategoria;

                        context.SaveChanges();
                        result.Object = productoToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Producto no encontrado.";
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

        public static DL.Result Delete(int idProducto)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext(new DbContextOptions<InnotecContext>()))
                {
                    var productoToDelete = context.Productos.FirstOrDefault(p => p.IdProductos == idProducto);
                    if (productoToDelete != null)
                    {
                        context.Productos.Remove(productoToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Producto no encontrado.";
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
