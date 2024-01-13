using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant>? getListRestaurants(int page, int pageSize, string sortBy, bool isDescending, int? humanId, string? city);
        Restaurant? getRestaurantById(int restaurantId);
        IEnumerable<Restaurant>? getRestaurantsByIdentifier(string restaurantName);
        IEnumerable<Restaurant>? getAllForOneOwner(int ownerId);
        int getTotalRestaurants();
        int createRestaurant(Restaurant restaurant);
        bool updateRestaurant(int restaurantId, Restaurant restaurant);
        bool deleteRestaurant(int restaurantId);
    }
}
