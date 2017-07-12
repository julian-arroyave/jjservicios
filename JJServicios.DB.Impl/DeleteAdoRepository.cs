using System;
using System.Configuration;
using System.Data.SqlClient;
using JJServicios.DB.Contracts.Repositories;

namespace JJServicios.DB.Impl
{
    public class DeleteAdoRepository : IDeleteAdoRepository
    {
        public void DeleteItemById(long id, string itemName)
        {
            int rowsUpdated = 0;

                using (
                    var connection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["JJServiciosEntities"].ConnectionString))
                {
                    string query = @"delete from [dbo].{0}
                                           where Id = @Id";

                    query = string.Format(query, itemName);

                    connection.Open();

                    var sqlCommand = new SqlCommand(query, connection);

                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    rowsUpdated = sqlCommand.ExecuteNonQuery();
                }
        }
    }
}
