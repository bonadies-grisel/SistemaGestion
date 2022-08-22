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
        public const string ConnectionString = "Server = DESKTOP-1L9TTLS;Database=SistemaGestion;Trusted_Connection=True";

        //Obtener todos los usuarios
        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario", sqlConnection))
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


        public static bool Login(string UName, string Pass)
        {
            //Bolean para aceptar el login
            bool loginSuccesful = false;

            //Creo la conexión con la base de datos
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))

            {
                //Creo los parámetros

                //Usuario
                SqlParameter parametroNombreUsuario = new SqlParameter("Uname", SqlDbType.VarChar) { Value = UName };


                //Contraseña
                SqlParameter parametroContraseña = new SqlParameter("Pass", SqlDbType.VarChar) { Value = Pass };


               

                string QueryUName = "SELECT * FROM Usuario WHERE @Uname = NombreUsuario";
                string QueryPass = "SELECT * FROM Usuario WHERE @Pass = Contraseña";

                sqlConnection.Open();

                using (SqlCommand sqlCommandUName = new SqlCommand(QueryUName, sqlConnection))

                  
                {
                    if (sqlCommandUName.Parameters.Contains(UName))

                    {
                        using (SqlCommand sqlCommandPass = new SqlCommand(QueryPass, sqlConnection))
                        
                        {
                            if(sqlCommandPass.Parameters.Contains(Pass))

                            {
                                loginSuccesful = true;

                            }

                            else

                            {
                                loginSuccesful = false; 
                            }
                        }
                    }

                    else
                    
                    {
                        loginSuccesful = false;
                    }


                }

                sqlConnection.Close();

            }

            return loginSuccesful;
        }



        public static bool DeleteUsuario(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Usuario WHERE Id = @id";

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

        public static bool CreateUser(Usuario usuario)
        {
            bool resultado = false;
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
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }


        public static bool ModificarNombreDeUsuario(Usuario usuario)
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



