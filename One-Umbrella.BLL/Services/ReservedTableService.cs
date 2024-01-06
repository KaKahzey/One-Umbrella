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
    public class ReservedTableService : IReservedTableService
    {
        private readonly IReservedTableRepository _reservedTableRepository;

        public ReservedTableService(IReservedTableRepository reservedTableRepository)
        {
            _reservedTableRepository = reservedTableRepository;
        }

        public IEnumerable<ReservedTable> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date)
        {
            return _reservedTableRepository.getAllForOneRestaurantForOneDay(restaurantId, date);
        }
    }
}
