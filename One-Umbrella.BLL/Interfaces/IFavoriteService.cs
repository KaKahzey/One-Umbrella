using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IFavoriteService
    {
        IEnumerable<Restaurant> getAllForHuman(int id);
        bool create(Favorite favorite);
        bool delete(int humanId, int restaurantId);
    }
}
