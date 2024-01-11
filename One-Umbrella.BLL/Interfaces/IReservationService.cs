using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IReservationService
    {
        IEnumerable<Reservation> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date);
        IEnumerable<Reservation> getAllForOneHuman(int humanId);
        IEnumerable<Reservation> getAllByStatus(int restaurantId, int status);
        bool changeStatus(int reservationId, int status);
        int create(Reservation reservation);
        bool delete(int reservationId);
    }
}
