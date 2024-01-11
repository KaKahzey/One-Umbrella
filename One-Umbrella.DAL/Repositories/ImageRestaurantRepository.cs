using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OneUmbrella.DAL.Repositories
{
    public class ImageRestaurantRepository : RepositoryBase<int, ImageRestaurant>, IImageRestaurantRepository
    {
        public ImageRestaurantRepository(SqlConnection dbConnection) : base(dbConnection) { }

        public ImageRestaurant getFrontImage(int restaurantId)
        {
            ImageRestaurant image = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM ImageRestaurant WHERE RESTAURANT_ID = @RestaurantId AND IS_FRONT = 1"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        image = new ImageRestaurant
                        {
                            ImageId = reader.GetInt32(reader.GetOrdinal("IMAGE_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            IsFront = reader.GetBoolean(reader.GetOrdinal("IS_FRONT")),
                            IsMenu = reader.GetBoolean(reader.GetOrdinal("IS_MENU"))

                        };
                        byte[] imageData = new byte[reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, null, 0, int.MaxValue)];
                        reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, imageData, 0, imageData.Length);
                        image.ImageData = imageData;
                    }
                }
            }
            connection.Close();
            return image;
        }

        public IEnumerable<ImageRestaurant> getAllForOneRestaurant(int restaurantId)
        {
            List<ImageRestaurant> images = new List<ImageRestaurant>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM ImageRestaurant WHERE RESTAURANT_ID = @RestaurantId"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ImageRestaurant image = new ImageRestaurant
                        {
                            ImageId = reader.GetInt32(reader.GetOrdinal("IMAGE_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            IsFront = reader.GetBoolean(reader.GetOrdinal("IS_FRONT")),
                            IsMenu = reader.GetBoolean(reader.GetOrdinal("IS_MENU"))

                        };
                        byte[] imageData = new byte[reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, null, 0, int.MaxValue)];
                        reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, imageData, 0, imageData.Length);
                        image.ImageData = imageData;
                        images.Add(image);
                    }
                }
            }
            connection.Close();
            return images;
        }
        public override IEnumerable<ImageRestaurant>? getAll()
        {
            List<ImageRestaurant> images = new List<ImageRestaurant>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM ImageRestaurant"
                , connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ImageRestaurant image = new ImageRestaurant
                        {
                            ImageId = reader.GetInt32(reader.GetOrdinal("IMAGE_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            IsFront = reader.GetBoolean(reader.GetOrdinal("IS_FRONT")),
                            IsMenu = reader.GetBoolean(reader.GetOrdinal("IS_MENU"))

                        };
                        byte[] imageData = new byte[reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, null, 0, int.MaxValue)];
                        reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, imageData, 0, imageData.Length);
                        image.ImageData = imageData;
                        images.Add(image);
                    }
                }
            }
            connection.Close();
            return images;
        }
        public override ImageRestaurant? getById(int imageId)
        {
            ImageRestaurant image = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM ImageRestaurant WHERE IMAGE_ID = @ImageId"
                , connection))
            {
                command.Parameters.AddWithValue("@ImageId", imageId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        image = new ImageRestaurant
                        {
                            ImageId = reader.GetInt32(reader.GetOrdinal("IMAGE_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            IsFront = reader.GetBoolean(reader.GetOrdinal("IS_FRONT")),
                            IsMenu = reader.GetBoolean(reader.GetOrdinal("IS_MENU"))

                        };
                        byte[] imageData = new byte[reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, null, 0, int.MaxValue)];
                        reader.GetBytes(reader.GetOrdinal("IMAGE_DATA"), 0, imageData, 0, imageData.Length);
                        image.ImageData = imageData;
                    }
                }
            }
            connection.Close();
            return image;
        }
        public override int create(ImageRestaurant image)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO ImageRestaurant(" +
                "[RESTAURANT_ID]," +
                "[IMAGE_DATA]," +
                "[IS_FRONT])" +
                " OUTPUT INSERTED.IMAGE_ID" +
                " VALUES(" +
                "@RestaurantId," +
                "@ImageData," +
                "@IsFront," +
                "@IsMenu)"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", image.RestaurantId);
                command.Parameters.AddWithValue("@ImageData", image.ImageData);
                command.Parameters.AddWithValue("@IsFront", image.IsFront);
                command.Parameters.AddWithValue("@IsMenu", image.IsMenu);
                int insertedId = (int)command.ExecuteScalar();
                connection.Close();
                return insertedId;
            }
        }
        public override bool update(int imageId, ImageRestaurant image)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "UPDATE ImageRestaurant SET " +
                "[RESTAURANT_ID] = @RestaurantId," +
                "[IMAGE_DATA] = @ImageData," +
                "[IS_FRONT] = @IsFront " +
                "[IS_MENU] = @IsMenu " +
                "WHERE IMAGE_ID = @ImageId"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", image.RestaurantId);
                command.Parameters.AddWithValue("@ImageData", image.ImageData);
                command.Parameters.AddWithValue("@IsFront", image.IsFront);
                command.Parameters.AddWithValue("@IsMenu", image.IsMenu);
                command.Parameters.AddWithValue("@ImageId", image.ImageId);

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
        public override bool delete(int imageId)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE " +
                "FROM ImageRestaurant" +
                " WHERE [IMAGE_ID] = @ImageId"
                , connection))
            {
                command.Parameters.AddWithValue("@ImageId", imageId);
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
