using DL;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class PedidoService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var pedidos = context.Pedidos.ToList();
                    result.Results = pedidos.Cast<object>().ToList();
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

        public static DL.Result GetById(int idPedido)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var pedido = context.Pedidos.FirstOrDefault(p => p.IdPedido == idPedido);
                    if (pedido != null)
                    {
                        result.Object = pedido;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Pedido no encontrado.";
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

        public static DL.Result Insert(DL.Pedido pedido)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    context.Pedidos.Add(pedido);
                    context.SaveChanges();
                    result.Object = pedido;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    result.ErrorMessage += " Inner Exception: " + ex.InnerException.Message;
                }

                // Agregar detalles adicionales del pedido para diagnosticar
                result.ErrorMessage += $" Pedido ID: {pedido.IdPedido}, Usuario ID: {pedido.UsuarioId}";

                result.Ex = ex;
            }


            return result;
        }

        public static DL.Result Update(int id, DL.Pedido pedido)
        {
            var result = new DL.Result();
            id = (int)pedido.IdPedido;
            try
            {
                using (var context = new InnotecContext())
                {
                    var pedidoToUpdate = context.Pedidos.FirstOrDefault(p => p.IdPedido == id);
                    if (pedidoToUpdate != null)
                    {
                        pedidoToUpdate.FechaPedido = pedido.FechaPedido;
                        pedidoToUpdate.EstadoPedido = pedido.EstadoPedido;
                        pedidoToUpdate.IdCompra = pedido.IdCompra;

                        context.SaveChanges();
                        result.Object = pedidoToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Pedido no encontrado.";
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

        public static DL.Result Delete(int idPedido)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var pedidoToDelete = context.Pedidos.FirstOrDefault(p => p.IdPedido == idPedido);
                    if (pedidoToDelete != null)
                    {
                        context.Pedidos.Remove(pedidoToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Pedido no encontrado.";
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
