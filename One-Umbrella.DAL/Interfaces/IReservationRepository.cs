using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    internal interface IReservationRepository : ICrudRepository<int, Reservation>
    {
        IEnumerable<Reservation> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date);
        IEnumerable<Reservation> getAllPending(int restaurantId);
        bool changeStatus(int reservationId, int status);
    }
}
