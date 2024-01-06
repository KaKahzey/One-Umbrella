using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    public interface IGridRepository : ICrudRepository<int, Grid>
    {
        IEnumerable<Grid>? getAllForOneRestaurant(int restaurantId);
    }
}
