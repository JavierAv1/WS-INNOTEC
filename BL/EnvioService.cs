using DL;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class EnvioService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var envios = context.Envios.ToList();
                    result.Results = envios.Cast<object>().ToList();
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

        public static DL.Result GetById(int idEnvio)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var envio = context.Envios.FirstOrDefault(e => e.IdEnvio == idEnvio);
                    if (envio != null)
                    {
                        result.Object = envio;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Envío no encontrado.";
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

        public static DL.Result Insert(DL.Envio envio)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    context.Envios.Add(envio);
                    context.SaveChanges();
                    result.Object = envio;
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

        public static DL.Result Update(int id, DL.Envio envio)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var envioToUpdate = context.Envios.FirstOrDefault(e => e.IdEnvio == id);
                    if (envioToUpdate != null)
                    {
                        envioToUpdate.CodigoPostal = envio.CodigoPostal;
                        envioToUpdate.Estado = envio.Estado;
                        envioToUpdate.Calle = envio.Calle;
                        envioToUpdate.Colonia = envio.Colonia;
                        envioToUpdate.Municipio = envio.Municipio;
                        envioToUpdate.Numero = envio.Numero;
                        envioToUpdate.IdCompra = envio.IdCompra;
                        envioToUpdate.Status = envio.Status;

                        context.SaveChanges();
                        result.Object = envioToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Envío no encontrado.";
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

        public static DL.Result Delete(int idEnvio)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var envioToDelete = context.Envios.FirstOrDefault(e => e.IdEnvio == idEnvio);
                    if (envioToDelete != null)
                    {
                        context.Envios.Remove(envioToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Envío no encontrado.";
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
