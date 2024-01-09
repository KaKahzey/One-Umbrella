using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Interfaces
{
    public interface IHumanRepository :ICrudRepository<int, Human>
    {
        bool checkMailValidity(string mail);
        bool checkPhoneValidity(string phoneNumber);
        string? getHashPwd(int id);
        Human? getByIdentifier(string identifier);
    }
}
