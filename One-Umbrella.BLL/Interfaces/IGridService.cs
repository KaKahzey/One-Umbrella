using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IGridService
    {
        IEnumerable<Grid>? getAllForOneRestaurant(int restaurantId);
        Grid? getById(int gridId);
        bool create(Grid grid);
        bool delete(int gridId);
    }
}
