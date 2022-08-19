﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet(Name ="GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
        }
    }
}