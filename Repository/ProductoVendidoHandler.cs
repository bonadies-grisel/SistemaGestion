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
    public static class ProductoVendidoHandler 
    {
        public const string ConnectionString = "Server = DESKTOP-1L9TTLS;Database=SistemaGestion;Trusted_Connection=True";
        public static List<ProductoVendido> GetProductoVendidos()

        {
            List<ProductoVendido> ProductosVendidos = new List<ProductoVendido>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido ProductoVendido = new ProductoVendido();
                                ProductoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                ProductoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                ProductoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                ProductoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                ProductosVendidos.Add(ProductoVendido);
                            }
                        }
                    }

                    sqlConnection.Close();
                }

            }
            return ProductosVendidos;
        }

        public static bool CrearProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[ProductoVendido] " +
                    "(Stock, IdProducto, IdVenta) VALUES " +
                    "(@StockParameter, @IdProductoParameter, @IdVentaParameter);";

                SqlParameter StockParameter = new SqlParameter("StockParameter", SqlDbType.VarChar) { Value = productoVendido.Stock };
                SqlParameter IdProductoParameter = new SqlParameter("IdProductoParameter", SqlDbType.VarChar) { Value = productoVendido.IdProducto };
                SqlParameter IdVentaParameter = new SqlParameter("IdVentaParameter", SqlDbType.VarChar) { Value = productoVendido.IdVenta };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(StockParameter);
                    sqlCommand.Parameters.Add(IdProductoParameter);
                    sqlCommand.Parameters.Add(IdVentaParameter);


                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }



    }
}
