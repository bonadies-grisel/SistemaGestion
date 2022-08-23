
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestion.Modelos;
using System.Data;

namespace SistemaGestion.Repository
{
    public static class VentaHandler
    {
        public const string ConnectionString = "Server = DESKTOP-1L9TTLS;Database=SistemaGestion;Trusted_Connection=True";

        //Obtener todaas las ventas
        public static List<Venta> GetVentas()
        {
            List<Venta> Ventas = new List<Venta>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();
                                Ventas.Add(venta);
                            }
                        }
                    }

                    sqlConnection.Close();
                }

            }
            return Ventas;
        }

        //Agregar ventas
        public static bool CreateSale(List<Producto> productos, Venta venta, ProductoVendido productovendido)
        {

            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Venta] " +
                    "(Comentarios) VALUES " +
                    "(@ComentariosParameter)";
                
                string queryInsertProductoVendido = "INSERT INTO [SistemaGestion].[dbo].[ProductoVendido]" +
                    "(Stock, IdProducto, IdVenta) VALUES" +
                    "(@StockParameter, @IdProductoParameter, @IdVentaParameter)";

                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto]" +
                                     "SET Stock = Stock - @StockProductParameter" +
                                     "WHERE id = @IdProducto";


                SqlParameter ComentariosParameter = new SqlParameter("comentariosParameter", SqlDbType.VarChar) { Value = venta.Comentarios};
                SqlParameter StockParameter = new SqlParameter("StockParameter", SqlDbType.Int) { Value = productovendido.Stock};
                SqlParameter IdProductoParameter = new SqlParameter("IdProductoParameter", SqlDbType.Int) { Value = productovendido.IdProducto };
                SqlParameter IdVentaParameter = new SqlParameter("IdVentaParameter", SqlDbType.Int) { Value = productovendido.IdVenta };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))

                {
                    sqlCommand.Parameters.Add(ComentariosParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    
                    if (numberOfRows > 0)
                         {
                        resultado = true;
                        }

                    foreach (Producto producto in productos)
                        
                            {
                                 using (SqlCommand sqlCommand2 = new SqlCommand(queryInsertProductoVendido, sqlConnection))
                                {
                                  sqlCommand2.Parameters.Add(StockParameter);
                                  sqlCommand2.Parameters.Add(IdProductoParameter);
                                  sqlCommand2.Parameters.Add(IdVentaParameter);
                                    
                                        if (numberOfRows > 0)
                                        {
                                             resultado = true;
                                             using (SqlCommand sqlCommand3 = new SqlCommand(queryUpdate, sqlConnection))
                                                
                                                {
                                                    sqlCommand3.Parameters.Add(IdProductoParameter);
                                                    sqlCommand3.Parameters.Add(StockParameter);
                                                    int numberOfRows2 = sqlCommand.ExecuteNonQuery();

                                                    if (numberOfRows2 > 0)
                                                    {
                                                        resultado = true;
                                                    }
                                                    else
                                                    {
                                                        resultado = false;
                                                    }

                                }
                                                }
                                        }
                                  
                            }   

                 }

                sqlConnection.Close();

            }

            return resultado;
        }

    }
}




//using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
//{
//    sqlCommand.Parameters.Add(ComentariosParameter);

//    int numberOfRows = sqlCommand.ExecuteNonQuery(); 

//    if (numberOfRows > 0)
//    {
//        resultado = true;
//    }
//}

//sqlConnection.Close();