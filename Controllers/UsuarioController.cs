using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestion.Handlers;
using SistemaGestion.Modelos;

namespace SistemaGestion.Controllers
{
    public class UsuarioController : DbHandler
    {
        public Login(Usuario NombreUsuario, Usuario Contraseña, string inputNombreUsuario, string inputContraseña)
        {
            //Bolean para aceptar el login
            bool loginSuccesful = false;

            //Creo la conexión con la base de datos
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))

            {
                //Creo los parámetros

                //Contraseña
                SqlParameter parametroContraseña = new SqlParameter();
                parametroContraseña.ParameterName = "Contraseña";
                parametroContraseña.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroContraseña.Value = Contraseña;


                //Usuaario
                //Creo los parámetros
                SqlParameter parametroNombreUsuario = new SqlParameter();
                parametroNombreUsuario.ParameterName = "NombreUsuario";
                parametroNombreUsuario.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroNombreUsuario.Value = NombreUsuario;

                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña", sqlConnection))

                {
                    sqlConnection.Open();

                    try
                    {
                        if ((!inputNombreUsuario.Equals(NombreUsuario)))
                        {
                            loginSuccesful = false;
                            if (loginSuccesful == false)
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = 0;
                                usuario.Nombre = " ";
                                usuario.Apellido = " ";
                                usuario.NombreUsuario = " ";
                                usuario.Mail = " ";
                                usuario.Contraseña = " ";
                                return usuario;
                            }
                        }
                        else
                        {
                            if (!(inputContraseña.Equals(Contraseña)))

                            {
                                loginSuccesful = false;
                                if (loginSuccesful == false)
                                {
                                    Usuario usuario = new Usuario();
                                    usuario.Id = 0;
                                    usuario.Nombre = "";
                                    usuario.Apellido = "";
                                    usuario.NombreUsuario = "";
                                    usuario.Mail = "";
                                    usuario.Contraseña = "";
                                    return usuario;
                                }

                            }

                            else

                            {
                                loginSuccesful = true;

                                if (loginSuccesful == true)
                                {

                                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                                    {
                                        Usuario usuario = new Usuario();
                                        usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                        usuario.Nombre = dataReader["Nombre"].ToString();
                                        usuario.Apellido = dataReader["Apellido"].ToString();
                                        usuario.Mail = dataReader["Mail"].ToString();
                                        return usuario;
                                    }

                                }

                            }
                        }

                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    sqlConnection.Close();

                }
            }
            return usuario;    
        }


    }
}
