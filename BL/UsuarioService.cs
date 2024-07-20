using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace BL
{
    public class UsuarioService
    {
        public static DL.Result GetAll()
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    var usuarios = context.Usuarios
                        .Include(u => u.TipoUsuarioIdTipousuarioNavigation) 
                        .Select(u => new
                        {
                            UsuarioId = u.UsuarioId,
                            Nombre = u.Nombre,
                            ApellidoPaterno = u.ApellidoPaterno,
                            ApellidoMaterno = u.ApellidoMaterno,
                            FechaDeNacimiento = u.FechaDeNacimiento,
                            Sexo = u.Sexo,
                            UserName = u.UserName,
                            Correo = u.Correo,
                            Contraseña = u.Contraseña,
                            Telefono = u.Telefono,
                            Celular = u.Celular,
                            TipoUsuario = new  { u.TipoUsuarioIdTipousuario , u.TipoUsuarioIdTipousuarioNavigation.TipoUsuario1 },
                            FotoDePerfil = u.FotoDePerfil
                        })
                        .ToList();

                    result.Results = usuarios.Cast<object>().ToList();
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

        public static DL.Result GetById(int idUsuario)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var usuario = context.Usuarios.FirstOrDefault(u => u.UsuarioId == idUsuario);
                    if (usuario != null)
                    {
                        result.Object = usuario;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Usuario no encontrado.";
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

        public static DL.Result Insert(DL.Usuario usuario)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    usuario.TipoUsuarioIdTipousuario = 1;
                    var salt = CreateSalt(usuario.UserName);
                    usuario.Contraseña = GenerateHash(usuario.Contraseña, salt);
                
                    var newUser = new DL.Usuario
                    {
                        Nombre = usuario.Nombre,
                        ApellidoPaterno = usuario.ApellidoPaterno,
                        ApellidoMaterno = usuario.ApellidoMaterno,
                        FechaDeNacimiento = usuario.FechaDeNacimiento,
                        Sexo = usuario.Sexo,
                        UserName = usuario.UserName,
                        Correo = usuario.Correo,
                        Contraseña = usuario.Contraseña,
                        Telefono = usuario.Telefono,
                        Celular = usuario.Celular,
                        TipoUsuarioIdTipousuario = usuario.TipoUsuarioIdTipousuario,
                        FotoDePerfil = usuario.FotoDePerfil
                    };
                    context.Usuarios.Add(newUser);
                    context.SaveChanges();

                    result.Object = usuario;
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

    
        public static DL.Result Update(int id, DL.Usuario usuario)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    var usuarioToUpdate = context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
                    if (usuarioToUpdate != null)
                    {
                        
                        var salt = CreateSalt(usuarioToUpdate.UserName); 
                        usuario.Contraseña = GenerateHash(usuario.Contraseña, salt);

                        usuarioToUpdate.Nombre = usuario.Nombre;
                        usuarioToUpdate.ApellidoPaterno = usuario.ApellidoPaterno;
                        usuarioToUpdate.ApellidoMaterno = usuario.ApellidoMaterno;
                        usuarioToUpdate.FechaDeNacimiento = usuario.FechaDeNacimiento;
                        usuarioToUpdate.Sexo = usuario.Sexo;
                        usuarioToUpdate.UserName = usuario.UserName;
                        usuarioToUpdate.Correo = usuario.Correo;
                        usuarioToUpdate.Contraseña = usuario.Contraseña;
                        usuarioToUpdate.Telefono = usuario.Telefono;
                        usuarioToUpdate.Celular = usuario.Celular;
                        usuarioToUpdate.TipoUsuarioIdTipousuario = usuario.TipoUsuarioIdTipousuario;
                        usuarioToUpdate.FotoDePerfil = usuario.FotoDePerfil;

                        context.SaveChanges();

                        result.Object = usuarioToUpdate;
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Usuario no encontrado.";
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

        public static DL.Result Delete(int idUsuario)
        {
            var result = new DL.Result();

            try
            {
                using (var context = new InnotecContext())
                {
                    var usuarioToDelete = context.Usuarios.FirstOrDefault(u => u.UsuarioId == idUsuario);
                    if (usuarioToDelete != null)
                    {
                        context.Usuarios.Remove(usuarioToDelete);
                        context.SaveChanges();
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Usuario no encontrado.";
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

        public static DL.Result Login(string userNameOrEmail, string password)
        {
            var result = new DL.Result();
            try
            {
                using (var context = new InnotecContext())
                {
                    if (string.IsNullOrEmpty(userNameOrEmail))
                    {
                        throw new ArgumentException("Se requiere un nombre de usuario o correo.");
                    }

                    var usuario = context.Usuarios
                        .FirstOrDefault(u => u.UserName == userNameOrEmail || u.Correo == userNameOrEmail);

                    if (usuario != null)
                    {
                        var Salt = CreateSalt(usuario.UserName);

                        var hashedPassword = GenerateHash(password, Salt);

                        if (usuario.Contraseña == hashedPassword)
                        {
                            result.Object = usuario;
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Contraseña incorrecta.";
                        }
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Usuario no encontrado.";
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



        /// <summary>
        /// Metodos para cifrar contraseña
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
       // Métodos auxiliares para generar sal y hash
        private static string CreateSalt(string baseString)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(baseString));
                return Convert.ToBase64String(saltBytes);
            }
        }

        private static string GenerateHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = $"{salt}{password}";
                var saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hashedBytes = sha256.ComputeHash(saltedPasswordAsBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }



    }
}
