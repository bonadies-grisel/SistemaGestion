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
        [HttpGet("GetProductoVendido")]
        public List<ProductoVendido> GetProductoVendidos()
        {
            return ProductoVendidoHandler.GetProductoVendidos();
        }


        //Crear producto
        [HttpPost("CreateProductoVendido")]
        public bool CreateProductoVendido([FromBody] PostProductoVendido productoVendido)
        {
            try
            {
                return ProductoVendidoHandler.CrearProductoVendido(new ProductoVendido
                {
                    Stock = productoVendido.Stock,
                    IdProducto = productoVendido.IdProducto,
                    IdVenta = productoVendido.IdVenta,


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


