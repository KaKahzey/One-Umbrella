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
    public class RatingRepository : IRatingRepository
    {
        public RatingRepository(SqlConnection dbConnection)
        {
            connection = dbConnection;
        }
        protected SqlConnection connection;

        public IEnumerable<Rating> getAllByRestaurant(int restaurantId, bool isHuman)
        {
            List<Rating> ratings = new List<Rating>();
            SqlCommand command = null;
            connection.Open();
            if (isHuman)
            {
                command = new SqlCommand(
                "Select * FROM Rating Where HUMAN_ID = @HumanId", connection);
                command.Parameters.AddWithValue("@HumanId", restaurantId);

            }
            else
            {
                command = new SqlCommand(
                "Select * FROM Rating Where RESTAURANT_ID = @RestaurantId", connection);
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);

            }
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var rating = new Rating
                    {
                        HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                        RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                        Score = reader.GetByte(reader.GetOrdinal("SCORE")),
                        Comment = reader.GetString(reader.GetOrdinal("COMMENT"))
                    };

                    ratings.Add(rating);
                }
            }
            connection.Close();
            return ratings;
        }

        public decimal countForOneRestaurant(int restaurantId)
        {
            int count = 0;
            int i = 0;
            connection.Open();
            using(SqlCommand command = new SqlCommand(
                "Select * FROM Rating Where RESTAURANT_ID = @RestaurantId"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int score = reader.GetByte(reader.GetOrdinal("SCORE"));
                        count += score;
                        i++;
                    }
                }
            }
            connection.Close();
            return i == 0 ? 0 : (decimal)count / i;
        }

        public bool create(Rating rating)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO Rating(" +
                "[HUMAN_ID]," +
                "[RESTAURANT_ID]," +
                "[SCORE]," +
                "[COMMENT])" +
                " VALUES(" +
                "@HumanId," +
                "@RestaurantId," +
                "@Score," +
                "@Comment)"
                , connection))
            {
                command.Parameters.AddWithValue("@HumanId", rating.HumanId);
                command.Parameters.AddWithValue("@RestaurantId", rating.RestaurantId);
                command.Parameters.AddWithValue("@Score", rating.Score);
                command.Parameters.AddWithValue("@Comment", rating.Comment);
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
        public bool update(Rating rating)
        {
            connection.Open();
            using(SqlCommand command = new SqlCommand(
                "UPDATE RATING SET " +
                "[SCORE] = @Score, " +
                "[COMMENT] = @Comment " +
                "WHERE [HUMAN_ID] = @HumanId AND " +
                "[RESTAURANT_ID] = @RestaurantId", connection))
            {
                command.Parameters.AddWithValue("@HumanId", rating.HumanId);
                command.Parameters.AddWithValue("@RestaurantId", rating.RestaurantId);
                command.Parameters.AddWithValue("@Score", rating.Score);
                command.Parameters.AddWithValue("@Comment", rating.Comment);
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
                " FROM Rating" +
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
