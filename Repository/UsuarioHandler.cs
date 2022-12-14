using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestion.Modelos;
using System.Data;

namespace SistemaGestion.Repository
{
    public static class UsuarioHandler
    {
        public const string ConnectionString = DbHandler.ConnectionString;
        //Obtener todos los usuarios
        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [SistemaGestion].[dbo].[Usuario] ", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                                // ¿Debería traer la contraseña? 
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuarios.Add(usuario);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return usuarios;
        }

        //Traer usuario por ID
        public static Usuario GeUserById(string username)
        {
            Usuario usuario = new Usuario();

            string querySelect = "SELECT * FROM Usuario WHERE NombreUsuario = @username";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection))
                {
                    SqlParameter usernameParameter = new SqlParameter("username", System.Data.SqlDbType.VarChar) { Value = username };

                    sqlCommand.Parameters.Add(usernameParameter);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return usuario;
        }


        //Login
        public static Usuario Login (string UName, string Pass)
        {
            //Bolean para aceptar el login
            bool loginSuccesful = false;

            //Creo el usuario para devolver
            Usuario user = new Usuario();

            //Creo la conexión con la base de datos
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))

            {
                //Creo los parámetros

                //Usuario

                SqlParameter parametroNombreUsuario = new("UName", SqlDbType.VarChar) { Value = UName };

                //Contraseña
                SqlParameter parametroContraseña = new SqlParameter("Pass", SqlDbType.VarChar) { Value = Pass } ;

                string QueryPass = "SELECT * FROM [SistemaGestion].[dbo].[Usuario] WHERE  NombreUsuario = @UName AND Contraseña = @Pass";

                sqlConnection.Open();

                   using (SqlCommand sqlCommand = new SqlCommand(QueryPass, sqlConnection))

                        {
                            sqlCommand.Parameters.Add(parametroContraseña);
                            sqlCommand.Parameters.Add(parametroNombreUsuario);

                          
                         using (SqlDataReader reader = sqlCommand.ExecuteReader())
                          { 
                                if (reader.HasRows)

                                {
                                    loginSuccesful = true;

                                    
                                            while (reader.Read())
                                            {
                                                user.Id = Convert.ToInt32(reader["Id"]);
                                                user.Nombre = (reader["Nombre"]).ToString();
                                                user.Apellido = (reader["Apellido"]).ToString();
                                                user.NombreUsuario = (reader["NombreUsuario"]).ToString();
                                                user.Contraseña = (reader["Contraseña"]).ToString();
                                                user.Mail = (reader["Mail"]).ToString();
                                            }
                                        
                                    
                                }

                                else

                                {
                                    loginSuccesful = false;
                                }
                          }
                    }


                sqlConnection.Close();
            }

            if (!loginSuccesful)
            
            {
                return new Usuario();
            }
            
            else
            
            {
                return user;
            }


        }


        //Eliminar usuario
        public static bool DeleteUsuario(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM [SistemaGestion].[dbo].[Usuario]  WHERE Id = @id";

                SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                idParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }

        //Crear nuevo usuario
        public static bool CreateUser(Usuario usuario)
        {
            bool create = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))

            {

                //Hago mi query INSERT
                string queryInsert = "INSERT INTO UPDATE [SistemaGestion].[dbo].[Usuario] " +
                    "(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES " +
                    "(@NameParameter, @FirstNameParameter, @UserNameParameter, @PassParameter, @MailParameter);";


                //Creo los parámetros que utilizaré
                
                SqlParameter NameParameter = new SqlParameter("NameParameter", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter FirstNameParameter = new SqlParameter("FirstNameParameter", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter UserNameParameter = new SqlParameter("UserNameParameter", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter PassParameter = new SqlParameter("PassParameter", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter MailParameter = new SqlParameter("MailParameter", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    
                    sqlCommand.Parameters.Add(NameParameter);
                    sqlCommand.Parameters.Add(FirstNameParameter);
                    sqlCommand.Parameters.Add(UserNameParameter);
                    sqlCommand.Parameters.Add(PassParameter);
                    sqlCommand.Parameters.Add(MailParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        create = true;
                    }
                }

                sqlConnection.Close();
            }

            return create;
        }


        //Cambiar nombre 
        public static bool ModificarNombre(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Usuario] " +
                    "SET Nombre = @nombre" +
                    "WHERE Id = @id ";

                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(idParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery(); 

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }




    }
}



