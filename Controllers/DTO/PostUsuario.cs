﻿
//DTO: Data Transfer Object 
namespace SistemaGestion.Controllers.DTO
{
    public class PostUsuario
    {
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }
    }
}