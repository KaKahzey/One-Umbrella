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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<Reservation> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date)
        {
            return _reservationRepository.getAllForOneRestaurantForOneDay(restaurantId, date);
        }
        public IEnumerable<Reservation> getAllForOneHuman(int humanId)
        {
            return _reservationRepository.getAllForOneHuman(humanId);
        }
        public IEnumerable<Reservation> getAllByStatus(int restaurantId, int status)
        {
            return _reservationRepository.getAllByStatus(restaurantId, status);
        }
        public bool changeStatus(int reservationId, int status)
        {
            return _reservationRepository.changeStatus(reservationId, status);
        }
        public int create(Reservation reservation)
        {
            return _reservationRepository.create(reservation);
        }
        public bool delete(int reservationId)
        {
            return _reservationRepository.delete(reservationId);
        }
    }
}
