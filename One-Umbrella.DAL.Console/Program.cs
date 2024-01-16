using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.DAL.Repositories;
using OneUmbrella.Domain.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using OneUmbrella.BLL.Services;
using Azure;
using OneUmbrella.BLL.Interfaces;
using System.Globalization;
using System.Numerics;
using OneUmbrella.Server.DataTransferObjects;
using OneUmbrella.Server.DataTransferObjects.Mappers;
internal class Program
{
    static void Main(string[] args)
    {

        SqlConnection connection = new SqlConnection("Server=PC;DataBase=One-Umbrella;User ID=Kahzey;Password='Umbrella4321';Encrypt=True");
        RestaurantRepository rp = new RestaurantRepository(connection);
        RestaurantService r = new RestaurantService(rp);
        ImageRestaurantRepository ip = new ImageRestaurantRepository(connection);
        ImageRestaurantService i = new ImageRestaurantService(ip);
        RatingRepository ratp = new RatingRepository(connection);
        RatingService rs = new RatingService(ratp);
        List<ListRestaurantDTO> restaurantsDTO = new List<ListRestaurantDTO>();
        IEnumerable<Restaurant>? restaurants = r.getListRestaurants(1, 5, "restaurant_name", false, null, null);
        foreach (Restaurant resto in restaurants)
        {
            resto.RestaurantRating = ratp.countForOneRestaurant(resto.RestaurantId);
            if (i.getFrontImage(resto.RestaurantId) != null)
            {
                string image = i.getFrontImage(resto.RestaurantId).ToDTO().ImageData;
                restaurantsDTO.Add(ListRestaurantMapper.ToDTO(resto, rs.getAllByRestaurant(resto.RestaurantId, false).Count(), image));
            }
            else
            {
                restaurantsDTO.Add(ListRestaurantMapper.ToDTO(resto, rs.getAllByRestaurant(resto.RestaurantId, false).Count(), "")); ;
            }
            
        }
        IEnumerable<ListRestaurantDTO> convertedRestaurants = restaurantsDTO;
        foreach (ListRestaurantDTO conv in convertedRestaurants)
        {
            Console.WriteLine(conv.RestaurantRating);
        }
        Console.WriteLine(ratp.countForOneRestaurant(1));
        //foreach (var e in restaurantRepository.getByPageAndSorted(1, 10, "RESTAURANT_NAME", false, null, null))
        //{
        //    Console.WriteLine(e.RestaurantName);
        //}

    }
}
