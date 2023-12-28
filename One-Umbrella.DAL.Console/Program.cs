using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.DAL.Repositories;
using OneUmbrella.Domain.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

internal class Program
{
    static void Main(string[] args)
    {

        SqlConnection connection = new SqlConnection("Data Source=PC;Initial Catalog=One-Umbrella;User ID=Kahzey;Password=Umbrella4321;Encrypt=True");
        IHumanRepository humanRepository = new HumanRepository(connection);
        Human human = new Human();
        human.HumanLastName = "newToto";
        human.HumanFirstName = "LastToto";
        human.HumanMail = "lasttoto@zzmail.toto";
        human.HumanPassword = "test12345";
        human.HumanPhoneNumber = "0376323564";
        human.HumanType = "Customer";
    }
}
