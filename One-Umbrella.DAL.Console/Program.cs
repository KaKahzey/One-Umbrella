using Microsoft.Data.SqlClient;

internal class Program
{
    static void Main(string[] args)
    {
        using (SqlConnection connection = new SqlConnection("Data Source=PC;Initial Catalog=One-Umbrella;User ID=Kahzey;Password=Umbrella4321;Encrypt=True"))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Human]", connection))
            {
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Console.WriteLine(reader["HUMAN_FIRSTNAME"]);
                    }
                }
            }


            connection.Close();
        }
    }
}
