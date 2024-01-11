using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Repositories
{
    public class ReservedTableRepository :  IReservedTableRepository
    {
        public ReservedTableRepository(SqlConnection dbConnection)
        {
            connection = dbConnection;
        }
        protected SqlConnection connection;

        public IEnumerable<ReservedTable> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date)
        {
            List<ReservedTable> reservedTables = new List<ReservedTable>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM ReservedTable WHERE RESTAURANT_ID = @RestaurantId " +
                "AND (CONVERT(DATE, RESERVATION_TIME_START) = @Date OR " +
                "CONVERT(DATE, RESERVATION_TIME_END) = @Date)"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                command.Parameters.AddWithValue("@Date", date.Date);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReservedTable reservedTable = new ReservedTable
                        {
                            ReservationId = reader.GetInt32(reader.GetOrdinal("RESERVATION_ID")),
                            TableId = reader.GetInt32(reader.GetOrdinal("TABLE_ID"))
                        };
                        reservedTables.Add(reservedTable);
                    }
                }
            }
            connection.Close();
            return reservedTables;
        }
        
        public bool create(int reservationId, int tableId)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO Favorite(" +
                "[HUMAN_ID]," +
                "[RESTAURANT_ID])" +
                " VALUES(" +
                "@ReservationId," +
                "@TableId)"
                , connection))
            {
                command.Parameters.AddWithValue("@ReservationId", reservationId);
                command.Parameters.AddWithValue("@TableId", tableId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }
    }
}
