using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Controllers.DTO;
using SistemaGestion.Modelos;
using SistemaGestion.Repository;


namespace SistemaGestion.Controllers
{ 
    //Avisa que esta clase va a estar expuesta y que es un controller
    [ApiController]
    //Especificamos la ruta ya que nuestro controller tendrá un lugar específico (el final de la URL) (Borra el controller del nombre)
    [Route("[controller]")]
    //El controllerbase para exponer todas las clases que lo implementan
    public class UsuarioController : ControllerBase
    {

        //Esto es un endpoint
        //Arriba del método, para avisarle al controller que es un GET (trae cosas)


        [HttpGet("Login")]
        public  Usuario Login(string UName, string Pass)

        {
            return UsuarioHandler.Login(UName, Pass);
        }


        [HttpGet("GetUsers")]
        public List<Usuario> GetUsers()
        {
            return UsuarioHandler.GetUsuarios();
        }


        [HttpDelete("DeleteUsers")]
        public bool DeleteUser([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.DeleteUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut("ModifyUser")]
        public bool ModifyUser([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarNombre(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre
            });
        }

        [HttpPost("CreateUser")]
        public bool CreateUser([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CreateUser(new Usuario
                {
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }


         }

        
       

    }
}
