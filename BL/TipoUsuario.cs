using DL;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class TipoUsuarioService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var tiposUsuario = context.TipoUsuarios.ToList();
                    result.Results = tiposUsuario.Cast<object>().ToList();
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

        public static DL.Result GetById(int idTipoUsuario)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var tipoUsuario = context.TipoUsuarios.FirstOrDefault(t => t.IdTipousuario == idTipoUsuario);
                    if (tipoUsuario != null)
                    {
                        result.Object = tipoUsuario;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Tipo de usuario no encontrado.";
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

        public static DL.Result Insert(DL.TipoUsuario tipoUsuario)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    context.TipoUsuarios.Add(tipoUsuario);
                    context.SaveChanges();
                    result.Object = tipoUsuario;
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

        public static DL.Result Update(int id, DL.TipoUsuario tipoUsuario)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var tipoUsuarioToUpdate = context.TipoUsuarios.FirstOrDefault(t => t.IdTipousuario == id);
                    if (tipoUsuarioToUpdate != null)
                    {
                        tipoUsuarioToUpdate.TipoUsuario1 = tipoUsuario.TipoUsuario1;
                        context.SaveChanges();
                        result.Object = tipoUsuarioToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Tipo de usuario no encontrado.";
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

        public static DL.Result Delete(int idTipoUsuario)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var tipoUsuarioToDelete = context.TipoUsuarios.FirstOrDefault(t => t.IdTipousuario == idTipoUsuario);
                    if (tipoUsuarioToDelete != null)
                    {
                        context.TipoUsuarios.Remove(tipoUsuarioToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Tipo de usuario no encontrado.";
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
