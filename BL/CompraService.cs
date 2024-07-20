using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BL
{
    public class CompraService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var compras = context.Compras
                                         .Include(c => c.IdproductoNavigation) 
                                         .OrderBy(c => c.IdCompra) 
                                         .Select(c => new {
                                             IdCompra = c.IdCompra,
                                             NombreProducto = c.IdproductoNavigation.Nombre,
                                             DescripcionProducto = c.IdproductoNavigation.DescripcionDelProducto,
                                             ImagenProducto = c.IdproductoNavigation.ImagenDelProducto,
                                             Cantidad = c.Cantidad,
                                             FechaDeCompra = c.FechaDeCompra,
                                             FechaVencimiento = c.FechaVencimiento,
                                             Precio = c.IdproductoNavigation.Precio
                                         })
                                         .ToList();

                    result.Results = compras.Cast<object>().ToList();
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

        public static DL.Result GetById(int idCompra)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var compra = context.Compras
                      .Include(c => c.IdproductoNavigation) 
                      .OrderBy(c => c.IdCompra) 
                      .Select(c => new {
                          IdCompra = c.IdCompra,
                          NombreProducto = c.IdproductoNavigation.Nombre,
                          DescripcionProducto = c.IdproductoNavigation.DescripcionDelProducto,
                          ImagenProducto = c.IdproductoNavigation.ImagenDelProducto,
                          Cantidad = c.Cantidad,
                          FechaDeCompra = c.FechaDeCompra,
                          FechaVencimiento = c.FechaVencimiento,
                          Precio = c.IdproductoNavigation.Precio
                      })
                    .FirstOrDefault();
                    if (compra != null)
                    {
                        result.Object = compra;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Compra no encontrada.";
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

        public static DL.Result GetByUserId(int idUsuario)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    
                    var compras = context.Compras
                                        .Where(c => c.Idusuario == idUsuario)
                                        .Include(c => c.IdproductoNavigation) 
                     .OrderBy(c => c.IdCompra) 
                     .Select(c => new {
                         IdCompra = c.IdCompra,
                         NombreProducto = c.IdproductoNavigation.Nombre,
                         DescripcionProducto = c.IdproductoNavigation.DescripcionDelProducto,
                         ImagenProducto = c.IdproductoNavigation.ImagenDelProducto,
                         Cantidad = c.Cantidad,
                         FechaDeCompra = c.FechaDeCompra,
                         FechaVencimiento = c.FechaVencimiento,
                         Precio = c.IdproductoNavigation.Precio,
                         IdProducto = c.Idproducto
                     })
                     .ToList();

                    if (compras != null && compras.Count > 0)
                    {
                        result.Results = compras.Cast<object>().ToList(); 
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Compra no encontrada.";
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

        public static DL.Result Insert(int idUsuario, int idProducto)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                   
                    var existingCompra = context.Compras
                        .FirstOrDefault(c => c.Idusuario == idUsuario && c.Idproducto == idProducto);

                    if (existingCompra != null)
                    {
             
                        existingCompra.Cantidad += 1;
                    }
                    else
                    {
                  
                        var newCompra = new DL.Compra
                        {
                            Idusuario = idUsuario,
                            Idproducto = idProducto,
                            FechaDeCompra = DateOnly.FromDateTime(DateTime.Now),
                            FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                            Cantidad = 1
                        };
                        context.Compras.Add(newCompra);
                    }

                
                    context.SaveChanges();
                    result.Success = true;
                    result.Object = existingCompra ?? context.Compras.Local.FirstOrDefault(); 
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

        public static DL.Result Update(int id, DL.Compra compra)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var compraToUpdate = context.Compras.FirstOrDefault(c => c.IdCompra == id);
                    if (compraToUpdate != null)
                    {
                        compraToUpdate.FechaDeCompra = DateOnly.FromDateTime(DateTime.Now);
                        compraToUpdate.FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
                        compraToUpdate.Idusuario = compra.Idusuario;
                        compraToUpdate.Idproducto = compra.Idproducto;
                        compraToUpdate.Cantidad = compra.Cantidad;

                        context.SaveChanges();
                        result.Object = compraToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Compra no encontrada.";
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

        public static DL.Result Delete(int idCompra)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var compraToDelete = context.Compras.FirstOrDefault(c => c.IdCompra == idCompra);
                    if (compraToDelete != null)
                    {
                        context.Compras.Remove(compraToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Compra no encontrada.";
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
