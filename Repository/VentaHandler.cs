﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SistemaGestion.Modelos;

namespace SistemaGestion.Repository
{
    public static class VentaHandler 
    {
        public const string ConnectionString = "Server = DESKTOP-1L9TTLS;Database=SistemaGestion;Trusted_Connection=True";
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

    }

}
