using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects
{
    public class WholeReservationDTO
    {
        public IEnumerable<ReservationDTO> Reservations { get; set; }
        public IEnumerable<ReservedTable> ReservedTables { get; set; }
    }
}
