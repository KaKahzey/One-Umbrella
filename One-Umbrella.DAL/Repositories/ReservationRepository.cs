using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OneUmbrella.DAL.Repositories
{
    public class ReservationRepository : RepositoryBase<int, Reservation>, IReservationRepository
    {
        public ReservationRepository(SqlConnection dbConnection) : base(dbConnection) { }

        public IEnumerable<Reservation> getAllForOneRestaurantForOneDay(int restaurantId, DateTime date)
        {
            List<Reservation> reservations = new List<Reservation>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Reservation WHERE RESTAURANT_ID = @RestaurantId " +
                "AND (CONVERT(DATE, RESERVATION_TIME_START) = @Date OR " +
                "CONVERT(DATE, RESERVATION_TIME_END) = @Date)"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                command.Parameters.AddWithValue("@Date", date.Date);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation
                        {
                            ReservationId = reader.GetInt32(reader.GetOrdinal("RESERVATION_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            ReservationTimeStart = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_START")),
                            ReservationTimeEnd = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_END")),
                            ReservationStatus = reader.GetByte(reader.GetOrdinal("RESERVATION_STATUS"))

                        };
                        reservations.Add(reservation);
                    }
                }
            }
            connection.Close();
            return reservations;
        }
        public IEnumerable<Reservation> getAllForOneHuman(int humanId)
        {
            List<Reservation> reservations = new List<Reservation>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Reservation WHERE HUMAN_ID = @HumanId"
                , connection))
            {
                command.Parameters.AddWithValue("@HumanId", humanId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation
                        {
                            ReservationId = reader.GetInt32(reader.GetOrdinal("RESERVATION_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            ReservationTimeStart = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_START")),
                            ReservationTimeEnd = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_END")),
                            ReservationStatus = reader.GetByte(reader.GetOrdinal("RESERVATION_STATUS"))

                        };
                        reservations.Add(reservation);
                    }
                }
            }
            connection.Close();
            return reservations;
        }
        public IEnumerable<Reservation> getAllByStatus(int restaurantId, int status)
        {
            List<Reservation> reservations = new List<Reservation>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Reservation WHERE RESTAURANT_ID = @RestaurantId " +
                "AND RESERVATION_STATUS = @Status"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", restaurantId);
                command.Parameters.AddWithValue("@Status", status);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation
                        {
                            ReservationId = reader.GetInt32(reader.GetOrdinal("RESERVATION_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            ReservationTimeStart = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_START")),
                            ReservationTimeEnd = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_END")),
                            ReservationStatus = reader.GetByte(reader.GetOrdinal("RESERVATION_STATUS"))

                        };
                        reservations.Add(reservation);
                    }
                }
            }
            connection.Close();
            return reservations;
        }
        public bool changeStatus(int reservationId, int status)
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(
                "UPDATE Reservation SET [RESERVATION_STATUS] = @Status WHERE RESERVATION_ID = @ReservationId",
                connection))
            {
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@ReservationId", reservationId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    connection.Close();
                    return true;
                }
            }

            connection.Close();
            return false;
        }
        public override IEnumerable<Reservation>? getAll()
        {
            List<Reservation> reservations = new List<Reservation>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Reservation"
                , connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation
                        {
                            ReservationId = reader.GetInt32(reader.GetOrdinal("RESERVATION_ID")),
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            ReservationTimeStart = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_START")),
                            ReservationTimeEnd = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_END")),
                            ReservationStatus = reader.GetByte(reader.GetOrdinal("RESERVATION_STATUS"))

                        };
                        reservations.Add(reservation);
                    }
                }
            }
            connection.Close();
            return reservations;
        }
        public override Reservation? getById(int reservationId)
        {
            Reservation reservation = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Reservation WHERE RESERVATION_ID = @ReservationId"
                , connection))
            {
                command.Parameters.AddWithValue("@ReservationId", reservationId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservation = new Reservation
                        {
                            RestaurantId = reader.GetInt32(reader.GetOrdinal("RESTAURANT_ID")),
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            ReservationTimeStart = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_START")),
                            ReservationTimeEnd = reader.GetDateTime(reader.GetOrdinal("RESERVATION_TIME_END")),
                            ReservationStatus = reader.GetByte(reader.GetOrdinal("RESERVATION_STATUS"))

                        };
                    }
                }
            }
            connection.Close();
            return reservation;
        }
        public override int create(Reservation reservation)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO Reservation(" +
                "[RESTAURANT_ID]," +
                "[HUMAN_ID]," +
                "[RESERVATION_TIME_START]," +
                "[RESERVATION_TIME_END]," +
                "[RESERVATION_STATUS])" +
                " OUTPUT INSERTED.RESERVATION_ID" +
                " VALUES(" +
                "@RestaurantId," +
                "@HumanId," +
                "@ReservationTimeStart," +
                "@ReservationTimeEnd," +
                "@ReservationStatus)"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", reservation.RestaurantId);
                command.Parameters.AddWithValue("@HumanId", reservation.HumanId);
                command.Parameters.AddWithValue("@ReservationTimeStart", reservation.ReservationTimeStart);
                command.Parameters.AddWithValue("@ReservationTimeEnd", reservation.ReservationTimeEnd);
                command.Parameters.AddWithValue("@ReservationStatus", reservation.ReservationStatus);
                int insertedId = (int)command.ExecuteScalar();
                connection.Close();
                return insertedId;
            }
        }
        public override bool update(int reservationId, Reservation reservation)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "UPDATE Reservation SET " +
                "[RESTAURANT_ID] = @RestaurantId," +
                "[HUMAN_ID] = @HumanId," +
                "[RESERVATION_TIME_START] = @ReservationTimeStart," +
                "[RESERVATION_TIME_END] = @ReservationTimeEnd," +
                "[RESERVATION_STATUS] = @ReservationStatus " +
                "WHERE RESERVATION_ID = @ReservationId"
                , connection))
            {
                command.Parameters.AddWithValue("@RestaurantId", reservation.RestaurantId);
                command.Parameters.AddWithValue("@HumanId", reservation.HumanId);
                command.Parameters.AddWithValue("@ReservationTimeStart", reservation.ReservationTimeStart);
                command.Parameters.AddWithValue("@ReservationTimeEnd", reservation.ReservationTimeEnd);
                command.Parameters.AddWithValue("@ReservationStatus", reservation.ReservationStatus);
                command.Parameters.AddWithValue("@ReservationId", reservationId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }
        public override bool delete(int reservationId)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE" +
                " FROM Reservation" +
                " WHERE [RESERVATION_ID] = @ReservationId"
                , connection))
            {
                command.Parameters.AddWithValue("@ReservationId", reservationId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }
    }
}
