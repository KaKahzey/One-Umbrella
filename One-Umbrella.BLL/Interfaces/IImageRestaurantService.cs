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
        ImageRestaurant? getFrontImage(int restaurantId);
        IEnumerable<ImageRestaurant> getAllForOneRestaurant(int restaurantId);
        int create(ImageRestaurant image);
        bool delete(int imageId);
    }
}
