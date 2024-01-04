using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Repositories
{
    public class GridRepository : RepositoryBase<int, Grid>, IGridRepository
    {
        public GridRepository(SqlConnection dbConnection) : base(dbConnection) { }

        public IEnumerable<Grid>? getAllForOneRestaurant(int restaurantId)
        {
            List<Grid> grids = new List<Grid>();
            connection.Open();
            using(SqlCommand command = new SqlCommand(
                "SELECT * FROM Grid WHERE RESTAURANT_ID = @RestaurantId"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Grid grid = new Grid
                        {
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            GridName = reader.GetString(reader.GetOrdinal("GRID_NAME")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            GridRows = reader.GetInt32(reader.GetOrdinal("GRID_ROWS")),
                            GridColumns = reader.GetInt32(reader.GetOrdinal("GRID_COLUMNS"))

                        };
                        grids.Add(grid);
                    }
                }
            }
            connection.Close();
            return grids;
        }
        public override IEnumerable<Grid>? getAll()
        {
            List<Grid> grids = new List<Grid>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Grid"
                , connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Grid grid = new Grid
                        {
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            GridName = reader.GetString(reader.GetOrdinal("GRID_NAME")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            GridRows = reader.GetInt32(reader.GetOrdinal("GRID_ROWS")),
                            GridColumns = reader.GetInt32(reader.GetOrdinal("GRID_COLUMNS"))

                        };
                        grids.Add(grid);
                    }
                }
            }
            connection.Close();
            return grids;
        }
        public override Grid? getById(int id)
        {
            Grid grid = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Grid WHERE GRID_ID = @GridId"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        grid = new Grid
                        {
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            GridName = reader.GetString(reader.GetOrdinal("GRID_NAME")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            GridRows = reader.GetInt32(reader.GetOrdinal("GRID_ROWS")),
                            GridColumns = reader.GetInt32(reader.GetOrdinal("GRID_COLUMNS"))
                        };
                    }
                }
            }
            connection.Close();
            return grid;
        }
        public override bool create(Grid grid)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO Grid(" +
                "[GRID_NAME]," +
                "[RESTAURANT_ID]," +
                "[GRID_ROWS]," +
                "[GRID_COLUMNS])" +
                " VALUES(" +
                "@GridName," +
                "@RestaurantId," +
                "@GridRows," +
                "@GridColumns)"
                , connection))
            {
                command.Parameters.AddWithValue("@GridName", grid.GridName);
                command.Parameters.AddWithValue("@RestaurantId", grid.RestaurantId);
                command.Parameters.AddWithValue("@GridRows", grid.GridRows);
                command.Parameters.AddWithValue("@GridColumns", grid.GridColumns);
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
        public override bool update(int gridId, Grid grid)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "UPDATE GRID SET " +
                "[GRID_NAME] = @GridName," +
                "[RESTAURANT_ID] = @RestaurantId," +
                "[GRID_ROWS] = @GridRows," +
                "[GRID_COLUMNS] = @GridColumns " +
                "WHERE GRID_ID = @GridId"
                , connection))
            {
                command.Parameters.AddWithValue("@GridName", grid.GridName);
                command.Parameters.AddWithValue("@RestaurantId", grid.RestaurantId);
                command.Parameters.AddWithValue("@GridRows", grid.GridRows);
                command.Parameters.AddWithValue("@GridColumns", grid.GridColumns);
                command.Parameters.AddWithValue("@GridId", gridId);
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
        public override bool delete(int gridId)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE" +
                " FROM Grid" +
                " WHERE [GRID_ID] = @GridId"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", gridId);
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
