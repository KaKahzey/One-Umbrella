using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class ReservationMapper
    {
        public static ReservationDTO ToDTO(this Reservation reservation)
        {
            return new ReservationDTO()
            {
                ReservationId = reservation.ReservationId,
                RestaurantId = reservation.RestaurantId,
                HumanId = reservation.HumanId,
                ReservationTimeStart = reservation.ReservationTimeStart,
                ReservationTimeEnd = reservation.ReservationTimeEnd,
                ReservationStatus = reservation.ReservationStatus
            };
        }
        public static Reservation ToEntity(this ReservationDataDTO reservation)
        {
            return new Reservation()
            {
                RestaurantId = reservation.RestaurantId,
                HumanId = reservation.HumanId,
                ReservationTimeStart = reservation.ReservationTimeStart,
                ReservationTimeEnd = reservation.ReservationTimeEnd
            };
        }
    }
}
