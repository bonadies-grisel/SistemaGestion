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
        [HttpGet(Name ="GetUsers")]
        public List<Usuario> GetUsers()
        {
            return UsuarioHandler.GetUsuarios();
        }


        [HttpDelete(Name = "DeleteUsers")]
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

        [HttpPut(Name = "ModifyUser")]
        public bool ModifyUser([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarNombreDeUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre
            });
        }

        [HttpPost(Name ="CreateUser")]
        public bool CreateUser([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CreateUser(new Usuario
                {
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    
                    
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
