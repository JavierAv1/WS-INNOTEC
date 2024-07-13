using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class ProveedorService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var proveedores = context.Proveedors.ToList();
                    result.Results = proveedores.Cast<object>().ToList();
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

        public static DL.Result GetById(int idProveedor)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var proveedor = context.Proveedors.FirstOrDefault(p => p.IdProveedor == idProveedor);
                    if (proveedor != null)
                    {
                        result.Object = proveedor;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Proveedor no encontrado.";
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

        public static DL.Result Insert(DL.Proveedor proveedor)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    context.Proveedors.Add(proveedor);
                    context.SaveChanges();
                    result.Object = proveedor;
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

        public static DL.Result Update(int id,DL.Proveedor proveedor)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var proveedorToUpdate = context.Proveedors.FirstOrDefault(p => p.IdProveedor == id);
                    if (proveedorToUpdate != null)
                    {
                        proveedorToUpdate.Nombre = proveedor.Nombre;
                        proveedorToUpdate.Telefono = proveedor.Telefono;
                        proveedorToUpdate.Direccion = proveedor.Direccion;

                        context.SaveChanges();
                        result.Object = proveedorToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Proveedor no encontrado.";
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

        public static DL.Result Delete(int idProveedor)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var proveedorToDelete = context.Proveedors.FirstOrDefault(p => p.IdProveedor == idProveedor);
                    if (proveedorToDelete != null)
                    {
                        context.Proveedors.Remove(proveedorToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Proveedor no encontrado.";
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
