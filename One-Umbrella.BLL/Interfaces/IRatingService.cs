using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IRatingService
    {
        IEnumerable<Rating> getAllByRestaurant(int id, bool isHuman);
        int countForOneRestaurant(int restaurantId);
        bool create(Rating rating);
        bool update(Rating rating);
        bool delete(int humanId, int restaurantId);
    }
}
