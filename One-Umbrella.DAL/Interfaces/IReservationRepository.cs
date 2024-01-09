using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    public interface IReservationRepository : ICrudRepository<int, Reservation>
    {
        IEnumerable<Reservation> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date);
        IEnumerable<Reservation> getAllForOneHuman(int humanId);
        IEnumerable<Reservation> getAllByStatus(int restaurantId, int status);
        bool changeStatus(int reservationId, int status);
    }
}
