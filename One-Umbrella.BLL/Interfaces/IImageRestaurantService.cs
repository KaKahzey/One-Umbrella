using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IImageRestaurantService
    {
        IEnumerable<ImageRestaurant> getAllForOneRestaurant(int restaurantId);
        bool create(ImageRestaurant image);
        bool delete(int imageId);
    }
}
