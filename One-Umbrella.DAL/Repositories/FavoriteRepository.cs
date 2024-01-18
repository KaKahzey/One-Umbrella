using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;

namespace OneUmbrella.DAL.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        public FavoriteRepository(SqlConnection dbConnection)
        {
            connection = dbConnection;
        }
        protected SqlConnection connection;

        public IEnumerable<Restaurant> getAllForHuman(int id)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT r.* FROM Restaurant r " +
                "JOIN Favorite f ON r.RESTAURANT_ID = f.RESTAURANT_ID " +
                "WHERE f.HUMAN_ID = @HumanId", connection))
            {
                command.Parameters.AddWithValue("@HumanId", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var restaurant = new Restaurant
                        {
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            RestaurantName = reader.GetString(reader.GetOrdinal("RESTAURANT_NAME")),
                            RestaurantStreet = reader.GetString(reader.GetOrdinal("RESTAURANT_STREET")),
                            RestaurantCity = reader.GetString(reader.GetOrdinal("RESTAURANT_CITY")),
                            RestaurantPostCode = reader.GetString(reader.GetOrdinal("RESTAURANT_POSTCODE")),
                            RestaurantDescription = reader.GetString(reader.GetOrdinal("RESTAURANT_DESCRIPTION")),
                            RestaurantRating = reader.GetDecimal(reader.GetOrdinal("RESTAURANT_RATING"))
                        };

                        restaurants.Add(restaurant);
                    }
                }
            }
            connection.Close();
            return restaurants;
        }
        public bool create(Favorite favorite)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO Favorite(" +
                "[HUMAN_ID]," +
                "[RESTAURANT_ID])" +
                " VALUES(" +
                "@HumanId," +
                "@RestaurantId)"
                , connection))
            {
                command.Parameters.AddWithValue("@HumanId", favorite.HumanId);
                command.Parameters.AddWithValue("@RestaurantId", favorite.RestaurantId);
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
        public bool delete(int humanId, int restaurantId)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE" +
                " FROM Favorite" +
                " WHERE [HUMAN_ID] = @HumanId AND [RESTAURANT_ID] = @RestaurantId"
                , connection))
            {
                command.Parameters.AddWithValue("@HumanId", humanId);
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);
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
