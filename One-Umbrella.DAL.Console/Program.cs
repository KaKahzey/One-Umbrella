using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.DAL.Repositories;
using OneUmbrella.Domain.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using OneUmbrella.BLL.Services;

internal class Program
{
    static void Main(string[] args)
    {

        SqlConnection connection = new SqlConnection("Server=PC;DataBase=One-Umbrella;User ID=Kahzey;Password='Umbrella4321';Encrypt=True");
        RestaurantRepository rr = new RestaurantRepository(connection);
        RestaurantService r = new RestaurantService(rr);
        foreach (var e in r.getListRestaurants(1, 5, "RESTAURANT_NAME", true, null, null))
        {
            Console.WriteLine(e.RestaurantCity);
        }

        //foreach (var e in restaurantRepository.getByPageAndSorted(1, 10, "RESTAURANT_NAME", false, null, null))
        //{
        //    Console.WriteLine(e.RestaurantName);
        //}

    }
}
