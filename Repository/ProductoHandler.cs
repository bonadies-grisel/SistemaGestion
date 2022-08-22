
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
    public static class ProductoHandler

    {
        public const string ConnectionString = "Server = DESKTOP-1L9TTLS;Database=SistemaGestion;Trusted_Connection=True";

        //Obtener todos los productos
        public static List<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                productos.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return productos;
        }


        //Eliminar producto
        internal static bool DeleteProduct(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Producto WHERE Id = @Id";

                SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numOfRows = sqlCommand.ExecuteNonQuery();
                    if (numOfRows > 0)
                    {

                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }

            return resultado;
        }

        //Crear nuevo producto
        public static bool CreateProduct(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))

            {

                //Hago mi query INSERT
                string queryInsert = "INSERT INTO UPDATE [SistemaGestion].[dbo].[Producto] " +
                    "(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES " +
                    "(@DescParameter, @CostoParameter, @PrecioParameter, @StockParameter, @IdUsuarioParameter);";


                //Creo los parámetros que utilizaré
                SqlParameter DescParameter = new SqlParameter("DescParameter", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter CostoParameter = new SqlParameter("CostoParameter", SqlDbType.Float) { Value = producto.Costo };
                SqlParameter PrecioParameter = new SqlParameter("PrecioParameter", SqlDbType.Float) { Value = producto.PrecioVenta };
                SqlParameter StockParameter = new SqlParameter("StockParameter", SqlDbType.Int) { Value = producto.Stock };
                SqlParameter IdUsuarioParameter = new SqlParameter("IdUsuarioParameter", SqlDbType.Int) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(DescParameter);
                    sqlCommand.Parameters.Add(CostoParameter);
                    sqlCommand.Parameters.Add(PrecioParameter);
                    sqlCommand.Parameters.Add(StockParameter);
                    sqlCommand.Parameters.Add(IdUsuarioParameter);

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


        //Modificar producto

        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Producto] " +
                    "SET Descripciones = @DescParameter, Costo = @CostoParameter, PrecioVenta = @PrecioParameter, Stock = @StockParameter, IdUsuario = @IdUsuarioParameter" +
                    "WHERE Id = @id ";

                SqlParameter IdParameter = new SqlParameter("IdParameter", SqlDbType.Int) { Value = producto.Id };
                SqlParameter DescParameter = new SqlParameter("DescParameter", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter CostoParameter = new SqlParameter("CostoParameter", SqlDbType.Float) { Value = producto.Costo };
                SqlParameter PrecioParameter = new SqlParameter("PrecioParameter", SqlDbType.Float) { Value = producto.PrecioVenta };
                SqlParameter StockParameter = new SqlParameter("StockParameter", SqlDbType.Int) { Value = producto.Stock };
                SqlParameter IdUsuarioParameter = new SqlParameter("IdUsuarioParameter", SqlDbType.Int) { Value = producto.IdUsuario };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(IdParameter);
                    sqlCommand.Parameters.Add(DescParameter);
                    sqlCommand.Parameters.Add(CostoParameter);
                    sqlCommand.Parameters.Add(PrecioParameter);
                    sqlCommand.Parameters.Add(StockParameter);
                    sqlCommand.Parameters.Add(IdUsuarioParameter);

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
