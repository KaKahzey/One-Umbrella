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
    public class GridService : IGridService
    {
        private readonly IGridRepository _gridRepository;

        public GridService(IGridRepository gridRepository)
        {
            _gridRepository = gridRepository;
        }

        public IEnumerable<Grid>? getAllForOneRestaurant(int restaurantId)
        {
            return _gridRepository.getAllForOneRestaurant(restaurantId);
        }
        public Grid? getById(int gridId)
        {
            return _gridRepository.getById(gridId);
        }
        public bool create(Grid grid)
        {
            return _gridRepository.create(grid);
        }
        public bool delete(int gridId)
        {
            return _gridRepository.delete(gridId);
        }

    }
}
