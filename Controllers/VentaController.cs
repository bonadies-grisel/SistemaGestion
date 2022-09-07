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



        [HttpGet( "GetVentas")]
        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas();
        }



        //Crear venta
        [HttpPost("CreateVenta")]
        public bool CreateVenta([FromBody] PostTest postTest)
        {
            try
            {
                var PostVenta = new Venta()

                {
                    Comentarios = postTest.PostVenta.Comentarios

                };
                var PostProductoVendido = new ProductoVendido()
                {
                    Stock = postTest.PostVendido.Stock,
                    IdProducto = postTest.PostVendido.IdProducto,
                    IdVenta = postTest.PostVendido.IdVenta,
                };

                return VentaHandler.CreateSale(postTest.productos, PostVenta, PostProductoVendido); 


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        ////Eliminar venta
        //[HttpDelete("DeleteVenta")]
        //public bool DeleteSale([FromBody] int id)

        //{
        //    try
        //    {
        //        VentaHandler.DeleteSale(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }

        //}

    }


    }

