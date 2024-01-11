using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    public interface IReservedTableRepository
    {
        IEnumerable<ReservedTable> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date);
        bool create(int reservationId, int tableId);


    }
}
