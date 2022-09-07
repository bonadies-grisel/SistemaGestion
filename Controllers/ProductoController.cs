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
    public class ProductoController : ControllerBase
    {

        //Obtener Productos
        [HttpGet("GetProducto")]
        public List<Producto> GetProducto()
        {
            return ProductoHandler.GetProductos();
        }



        //Crear producto
        [HttpPost("CreateProduct")]
        public bool CreateProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.CreateProduct(new Producto
                {
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario,


                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Borrar producto
        [HttpDelete("DeleteProduct")]
        public bool DeleteProduct([FromBody] int id)
        {
            try
            {
                return ProductoHandler.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        //Modificar Producto
        [HttpPut("ModifyProducto")]
        public bool ModifyUser([FromBody] PutProducto producto)
        {
            return ProductoHandler.ModificarProducto(new Producto
            {
                Id = producto.Id,
                Descripciones = producto.Descripciones,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario,
            });
        }

    }
}
