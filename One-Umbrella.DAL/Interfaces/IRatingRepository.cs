using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    public interface IRatingRepository
    {
        IEnumerable<Rating> getAllByRestaurant(int restaurantId, bool isHuman);
        int countForOneRestaurant(int restaurantId);
        bool create(Rating rating);
        bool update(Rating rating);
        bool delete(int humanId, int restaurantId);
    }
}
