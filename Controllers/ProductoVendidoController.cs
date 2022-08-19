using Microsoft.AspNetCore.Mvc;
using SistemaGestion.Modelos;
using SistemaGestion.Repository;

namespace SistemaGestion.Controllers
{
    //Avisa que esta clase va a estar expuesta y que es un controller
    [ApiController]
    //Especificamos la ruta ya que nuestro controller tendrá un lugar específico (el final de la URL) (Borra el controller del nombre)
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetProductoVendido")]
        public List<ProductoVendido> GetProductoVendidos()
        {
            return ProductoVendidoHandler.GetProductoVendidos();
        }
    }
}
