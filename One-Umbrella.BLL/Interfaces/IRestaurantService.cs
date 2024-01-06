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
        Restaurant? getRestaurant(int restaurantId);
        bool createRestaurant(Restaurant restaurant);
        bool updateRestaurant(int restaurantId, Restaurant restaurant);
        bool deleteRestaurant(int restaurantId);
    }
}
