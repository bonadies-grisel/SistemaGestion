using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ProyectoCoderHouse.Modelos;

namespace SistemaGestion.Handlers
{
    public class UsuarioHandler : DbHandler
    {
        public List<Usuario> GetUsuarios()
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

        public void Login (int id, string Contraseña)
        {
            bool loginSuccesful = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string Verificar = "SELECT Contraseña WHERE Id = @IdUsuario";

                SqlParameter parametroUsuarioId = new SqlParameter();
                parametroUsuarioId.ParameterName = "idUsuario";
                parametroUsuarioId.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroUsuarioId.Value = id;

                SqlParameter parametroContraseña = new SqlParameter();
                parametroUsuarioId.ParameterName = "Contraseña";
                parametroUsuarioId.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroUsuarioId.Value = Contraseña;

                sqlConnection.Open();

                { 
                    if ((Contraseña.Equals(Verificar)))
                        {
                            loginSuccesful = true;
                        }
                }
            }
            //Sé que una función void no puede retornar nada, sin embargo no sabía cómo declararla 
            return loginSuccesful;
        }
    }
}

     
    
