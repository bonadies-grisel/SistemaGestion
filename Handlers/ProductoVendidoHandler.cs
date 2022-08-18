using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestion.Modelos;

namespace SistemaGestion.Handlers
{
    public class ProductoVendidoHandler : DbHandler
    {
        public List<ProductoVendido> GetProductoVendidos()

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

    }
}
