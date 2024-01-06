using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.BLL.Interfaces
{
    public interface IUserService
    {
        bool checkEmail(string email);
        bool checkPhoneNumber(string phoneNumber);
        bool register(Human user);
        Human? login(string identifier, string password);
        Human? getUser(int userId);
        bool updateUser(int userId, Human user);
        
    }
}
