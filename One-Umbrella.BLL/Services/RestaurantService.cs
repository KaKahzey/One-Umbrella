using OneUmbrella.BLL.Interfaces;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public IEnumerable<Restaurant>? getListRestaurants(int page, int pageSize, string sortBy, bool isDescending, int? humanId, string? city)
        {
            return _restaurantRepository.getByPageAndSorted(page, pageSize, sortBy, isDescending, humanId, city);
        }
        public Restaurant? getRestaurantById(int restaurantId)
        {
            return _restaurantRepository.getById(restaurantId);
        }
        public IEnumerable<Restaurant>? getRestaurantsByIdentifier(string restaurantName)
        {
            return _restaurantRepository.getByIdentifier(restaurantName);
        }
        public int createRestaurant(Restaurant restaurant)
        {
            return _restaurantRepository.create(restaurant);
        }
        public bool updateRestaurant(int restaurantId, Restaurant restaurant)
        {
            return _restaurantRepository.update(restaurantId, restaurant);
        }
        public bool deleteRestaurant(int restaurantId)
        {
            return _restaurantRepository.delete(restaurantId);
        }
    }
}
