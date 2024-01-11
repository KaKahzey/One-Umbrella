using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Repositories
{
    public class HumanRepository : RepositoryBase<int, Human>, IHumanRepository 
    {
        public HumanRepository(SqlConnection dbConnection) : base(dbConnection) { }

        public bool checkMailValidity(string mail)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT [HUMAN_MAIL] FROM HUMAN WHERE [HUMAN_MAIL] = @Mail", connection))
            {
                command.Parameters.AddWithValue("@Mail", mail);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        connection.Close();
                        return true;
                    }
                }
            }
            connection.Close();
            return false;
        }
        public bool checkPhoneValidity(string phoneNumber)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT [HUMAN_PHONE_NUMBER] FROM HUMAN WHERE [HUMAN_PHONE_NUMBER] = @PhoneNumber", connection))
            {
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        connection.Close();
                        return true;
                    }
                }
            }
            connection.Close();
            return false;
        }
        public string? getHashPwd(int id)
        {
            string password = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT" +
                " [HUMAN_PASSWORD]" +
                " FROM HUMAN" +
                " WHERE [HUMAN_ID] = @Id"
                , connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        password = reader.GetString(reader.GetOrdinal("HUMAN_PASSWORD"));
                    }
                }
            }
            connection.Close();
            return password;
        }
        public Human? getByIdentifier(string identifier)
        {
            Human human = null;
            connection.Open();
            using(SqlCommand command = new SqlCommand(
                "SELECT" +
                " *" +
                " FROM HUMAN" +
                " WHERE [HUMAN_MAIL] = @Identifier OR [HUMAN_PHONE_NUMBER] = @Identifier"
                , connection))
            {
                command.Parameters.AddWithValue("@Identifier", identifier);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        human = new Human()
                        {
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            HumanLastName = reader.GetString(reader.GetOrdinal("HUMAN_LASTNAME")),
                            HumanFirstName = reader.GetString(reader.GetOrdinal("HUMAN_FIRSTNAME")),
                            HumanMail = reader.GetString(reader.GetOrdinal("HUMAN_MAIL")),
                            HumanPassword = reader.GetString(reader.GetOrdinal("HUMAN_PASSWORD")),
                            HumanPhoneNumber = reader.GetString(reader.GetOrdinal("HUMAN_PHONE_NUMBER")),
                            HumanDateInscription = reader.GetDateTime(reader.GetOrdinal("HUMAN_DATE_INSCRIPTION")),
                            HumanType = reader.GetString(reader.GetOrdinal("HUMAN_TYPE"))
                        };

                    }
                }
            }
            connection.Close();
            return human;
        }
        public override IEnumerable<Human>? getAll()
        {
            List<Human> list = new List<Human>();
            connection.Open();

            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM HUMAN", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Human human = new Human()
                        {
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            HumanLastName = reader.GetString(reader.GetOrdinal("HUMAN_LASTNAME")),
                            HumanFirstName = reader.GetString(reader.GetOrdinal("HUMAN_FIRSTNAME")),
                            HumanMail = reader.GetString(reader.GetOrdinal("HUMAN_MAIL")),
                            HumanPassword = reader.GetString(reader.GetOrdinal("HUMAN_PASSWORD")),
                            HumanPhoneNumber = reader.GetString(reader.GetOrdinal("HUMAN_PHONE_NUMBER")),
                            HumanDateInscription = reader.GetDateTime(reader.GetOrdinal("HUMAN_DATE_INSCRIPTION")),
                            HumanType = reader.GetString(reader.GetOrdinal("HUMAN_TYPE"))
                        };

                        list.Add(human);
                    }
                }
            }
            connection.Close();
            return list;
        }
        public override Human? getById(int id)
        {
            Human human = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT" +
                " *" +
                " FROM HUMAN" +
                " WHERE [HUMAN_ID] = @Id"
                , connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        human = new Human()
                        {
                            HumanId = reader.GetInt32(reader.GetOrdinal("HUMAN_ID")),
                            HumanLastName = reader.GetString(reader.GetOrdinal("HUMAN_LASTNAME")),
                            HumanFirstName = reader.GetString(reader.GetOrdinal("HUMAN_FIRSTNAME")),
                            HumanMail = reader.GetString(reader.GetOrdinal("HUMAN_MAIL")),
                            HumanPassword = reader.GetString(reader.GetOrdinal("HUMAN_PASSWORD")),
                            HumanPhoneNumber = reader.GetString(reader.GetOrdinal("HUMAN_PHONE_NUMBER")),
                            HumanDateInscription = reader.GetDateTime(reader.GetOrdinal("HUMAN_DATE_INSCRIPTION")),
                            HumanType = reader.GetString(reader.GetOrdinal("HUMAN_TYPE"))
                        };
                    }
                    }
                }
            connection.Close();
            return human;
        }
        public override int create(Human human)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO HUMAN(" +
                "[HUMAN_LASTNAME]," +
                "[HUMAN_FIRSTNAME]," +
                "[HUMAN_MAIL]," +
                "[HUMAN_PASSWORD]," +
                "[HUMAN_PHONE_NUMBER]," +
                "[HUMAN_TYPE])" +
                " OUTPUT INSERTED.HUMAN_ID" +
                " VALUES(" +
                "@LastName," +
                "@FirstName," +
                "@Mail," +
                "@Password," +
                "@PhoneNumber," +
                "@Type)"
                , connection))
            {
                command.Parameters.AddWithValue("@LastName", human.HumanLastName);
                command.Parameters.AddWithValue("@FirstName", human.HumanFirstName);
                command.Parameters.AddWithValue("@Mail", human.HumanMail);
                command.Parameters.AddWithValue("@Password", human.HumanPassword);
                command.Parameters.AddWithValue("@PhoneNumber", human.HumanPhoneNumber);
                command.Parameters.AddWithValue("@Type", human.HumanType);
                int insertedId = (int)command.ExecuteScalar();
                connection.Close();
                return insertedId;
            }
        }
        public override bool update(int id, Human updatedHuman)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "UPDATE HUMAN SET " +
                "[HUMAN_LASTNAME] = @LastName, " +
                "[HUMAN_FIRSTNAME] = @FirstName, " +
                "[HUMAN_MAIL] = @Mail, " +
                "[HUMAN_PASSWORD] = @Password, " +
                "[HUMAN_PHONE_NUMBER] = @PhoneNumber " +
                "WHERE [HUMAN_ID] = @Id"
                , connection))
            {
                command.Parameters.AddWithValue("@LastName", updatedHuman.HumanLastName);
                command.Parameters.AddWithValue("@FirstName", updatedHuman.HumanFirstName);
                command.Parameters.AddWithValue("@Mail", updatedHuman.HumanMail);
                command.Parameters.AddWithValue("@Password", updatedHuman.HumanPassword);
                command.Parameters.AddWithValue("@PhoneNumber", updatedHuman.HumanPhoneNumber);
                command.Parameters.AddWithValue("@Id", id);
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
        public override bool delete(int id)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE" +
                " FROM HUMAN" +
                " WHERE [HUMAN_ID] = @Id"
                , connection))
            {
                command.Parameters.AddWithValue("@Id", id);
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

