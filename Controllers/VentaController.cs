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
    public class VentaController : ControllerBase
    {



        [HttpGet(Name = "GetVentas")]
        public List<Venta> GetVentas([FromBody] PutVenta venta)
        {
            return VentaHandler.GetVentas();
        }



        //Crear venta
        [HttpPost(Name = "CreateVenta")]
        public bool CreateVenta([FromBody] List<Producto> productos, PostVenta venta, PostProductoVendido productovendido)
        {
            try
            {
                var PostVenta = new Venta()

                {
                    Comentarios = venta.Comentarios

                };
                var PostProductoVendido = new ProductoVendido()
                {
                    Stock = productovendido.Stock,
                    IdProducto = productovendido.IdProducto,
                    IdVenta = productovendido.IdVenta,
                };

                return VentaHandler.CreateSale(productos, PostVenta, PostProductoVendido); 


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }




    }
}
