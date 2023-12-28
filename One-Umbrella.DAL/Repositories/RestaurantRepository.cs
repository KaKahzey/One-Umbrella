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
    public class RestaurantRepository : RepositoryBase<int, Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(SqlConnection dbConnection) : base(dbConnection) { }

        public string? getAddressByIdentifier(string identifier)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT " +
                "[RESTAURANT_ID," +
                "[RESTAURANT_NAME]," +
                "[RESTAURANT_STREET]," +
                "[RESTAURANT_CITY]," +
                "[RESTAURANT_POSTCODE]" +
                " FROM Restaurant" +
                " WHERE [RESTAURANT_NAME] = @Identifier AND ["
                , connection))
            {

            }
        }
    }
}
