using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Repositories
{
    public class RestaurantRepository : RepositoryBase<int, Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(SqlConnection dbConnection) : base(dbConnection) { }
        public Restaurant? getByIdentifier(string identifier)
        {
            Restaurant restaurant = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT " +
                "*" +
                " FROM Restaurant" +
                " WHERE [RESTAURANT_NAME] = @Identifier " +
                "OR [RESTAURANT_CITY] = @Identifier"
                , connection))
            {
                command.Parameters.AddWithValue("@Identifier", identifier);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        restaurant = new Restaurant
                        {
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            RestaurantName = reader.GetString(reader.GetOrdinal("RESTAURANT_NAME")),
                            RestaurantStreet = reader.GetString(reader.GetOrdinal("RESTAURANT_STREET")),
                            RestaurantCity = reader.GetString(reader.GetOrdinal("RESTAURANT_CITY")),
                            RestaurantPostCode = reader.GetString(reader.GetOrdinal("RESTAURANT_POSTCODE")),
                            RestaurantDescription = reader.GetString(reader.GetOrdinal("RESTAURANT_DESCRIPTION")),
                            RestaurantRating = reader.GetByte(reader.GetOrdinal("RESTAURANT_RATING"))
                        };
                    }
                }
            }
            connection.Close();
            return restaurant;
        }
        public IEnumerable<Restaurant> getByPageAndSorted(int page, int pageSize, string sortBy, bool isDescending, int? humanId, string? city)
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                var sql = "SELECT * FROM (";
                sql += "SELECT *, ROW_NUMBER() OVER (ORDER BY ";
                sql += $"{sortBy} {(isDescending ? "DESC" : "ASC")}) AS RowNum FROM Restaurant WHERE 1 = 1";

                if (humanId.HasValue)
                {
                    sql += " AND HUMAN_ID = @HumanId";
                    command.Parameters.AddWithValue("@HumanId", humanId.Value);
                }
                if (!string.IsNullOrEmpty(city))
                {
                    sql += " AND [RESTAURANT_CITY] = @City";
                    command.Parameters.AddWithValue("@City", city);
                }

                sql += ") AS SubQuery ";
                sql += "WHERE RowNum BETWEEN @StartRow AND @EndRow";

                command.Parameters.AddWithValue("@StartRow", (page - 1) * pageSize + 1);
                command.Parameters.AddWithValue("@EndRow", page * pageSize);

                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    List<Restaurant> restaurants = new List<Restaurant>();

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
                            RestaurantRating = reader.GetByte(reader.GetOrdinal("RESTAURANT_RATING"))
                        };

                        restaurants.Add(restaurant);
                    }
                    connection.Close();
                    return restaurants;
                }
            }
        }
        public int getTotalRestaurants()
        {
            int total = 0;
            connection.Open();
            using(SqlCommand command = new SqlCommand(
                "SELECT " +
                "COUNT(*) " +
                "FROM Restaurant", connection))
            {
                total = (int)command.ExecuteScalar();
            }
            connection.Close();
            return total;
        }
        public override IEnumerable<Restaurant> getAll()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Restaurant", connection))
            {
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
                            RestaurantRating = reader.GetByte(reader.GetOrdinal("RESTAURANT_RATING"))
                        };

                        restaurants.Add(restaurant);
                    }
                }
            }
            connection.Close();
            return restaurants;
        }
        public override Restaurant? getById(int id)
        {
            Restaurant restaurant = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT " +
                "*" +
                " FROM Restaurant" +
                " WHERE [RESTAURANT_ID] = @Id"
                , connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        restaurant = new Restaurant
                        {
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            RestaurantName = reader.GetString(reader.GetOrdinal("RESTAURANT_NAME")),
                            RestaurantStreet = reader.GetString(reader.GetOrdinal("RESTAURANT_STREET")),
                            RestaurantCity = reader.GetString(reader.GetOrdinal("RESTAURANT_CITY")),
                            RestaurantPostCode = reader.GetString(reader.GetOrdinal("RESTAURANT_POSTCODE")),
                            RestaurantDescription = reader.GetString(reader.GetOrdinal("RESTAURANT_DESCRIPTION")),
                            RestaurantRating = reader.GetByte(reader.GetOrdinal("RESTAURANT_RATING"))
                        };
                    }
                }
            }
            connection.Close();
            return restaurant;
        }
        public override bool create(Restaurant restaurant)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO RESTAURANT(" +
                "[HUMAN_ID]," +
                "[RESTAURANT_NAME]," +
                "[RESTAURANT_STREET]," +
                "[RESTAURANT_CITY]," +
                "[RESTAURANT_POSTCODE]," +
                "[RESTAURANT_DESCRIPTION]," +
                "[RESTAURANT_RATING])" +
                " VALUES(" +
                "@HumanId," +
                "@RestaurantName," +
                "@RestaurantStreet," +
                "@RestaurantCity," +
                "@RestaurantPostCode," +
                "@RestaurantDescription," +
                "@RestaurantRating)"
                , connection))
            {
                command.Parameters.AddWithValue("@HumanId", restaurant.HumanId);
                command.Parameters.AddWithValue("@RestaurantName", restaurant.RestaurantName);
                command.Parameters.AddWithValue("@RestaurantStreet", restaurant.RestaurantStreet);
                command.Parameters.AddWithValue("@RestaurantCity", restaurant.RestaurantCity);
                command.Parameters.AddWithValue("@RestaurantPostCode", restaurant.RestaurantPostCode);
                command.Parameters.AddWithValue("@RestaurantDescription", restaurant.RestaurantDescription);
                command.Parameters.AddWithValue("@RestaurantRating", restaurant.RestaurantRating);
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
        public override bool update(int id, Restaurant restaurant)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "UPDATE RESTAURANT SET " +
                "[HUMAN_ID] = @HumanId, " +
                "[RESTAURANT_NAME] = @RestaurantName, " +
                "[RESTAURANT_STREET] = @RestaurantStreet, " +
                "[RESTAURANT_CITY] = @RestaurantCity, " +
                "[RESTAURANT_POSTCODE] = @RestaurantPostCode, " +
                "[RESTAURANT_DESCRIPTION] = @RestaurantDescription, " +
                "[RESTAURANT_RATING] = @RestaurantRating " +
                "WHERE [RESTAURANT_ID] = @RestaurantId"
                , connection))
            {
                command.Parameters.AddWithValue("@HumanId", restaurant.HumanId);
                command.Parameters.AddWithValue("@RestaurantName", restaurant.RestaurantName);
                command.Parameters.AddWithValue("@RestaurantStreet", restaurant.RestaurantStreet);
                command.Parameters.AddWithValue("@RestaurantCity", restaurant.RestaurantCity);
                command.Parameters.AddWithValue("@RestaurantPostCode", restaurant.RestaurantPostCode);
                command.Parameters.AddWithValue("@RestaurantDescription", restaurant.RestaurantDescription);
                command.Parameters.AddWithValue("@RestaurantRating", restaurant.RestaurantRating);
                command.Parameters.AddWithValue("@RestaurantId", id);

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
        public override bool delete(int id)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE" +
                " FROM RESTAURANT" +
                " WHERE [RESTAURANT_ID] = @Id"
                , connection))
            {
                command.Parameters.AddWithValue("@Id", id);
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
