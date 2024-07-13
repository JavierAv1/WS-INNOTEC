using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                    var compras = context.Compras.ToList();
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
                    var compra = context.Compras.FirstOrDefault(c => c.IdCompra == idCompra);
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

        public static DL.Result Insert(DL.Compra compra)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    compra.FechaDeCompra = DateOnly.FromDateTime(DateTime.Now);
                    compra.FechaVencimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(30));


                    context.Compras.Add(compra);
                    context.SaveChanges();
                    result.Object = compra;
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
