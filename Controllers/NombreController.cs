using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]   
    public class NombreController : ControllerBase
    {
        /
        [HttpGet]
        public string GetNombreLocal()
        {
            return "El nombre que quieras";
        }        
    }
}
