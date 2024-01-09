using OneUmbrella.BLL.Interfaces;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.DAL.Repositories;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;

namespace OneUmbrella.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IHumanRepository _humanRepository;

        public UserService(IHumanRepository humanRepository)
        {
            _humanRepository = humanRepository;
        }

        public bool checkEmail(string email)
        {
            return !_humanRepository.checkMailValidity(email);
        }
        public bool checkPhoneNumber(string phoneNumber)
        {
            return !_humanRepository.checkPhoneValidity(phoneNumber);
        }
        public bool register(Human user)
        {
            if (checkEmail(user.HumanMail) && checkPhoneNumber(user.HumanPhoneNumber))
            {
                user.HumanPassword = Argon2.Hash(user.HumanPassword);
                return _humanRepository.create(user);
            }
            return false;
        }
        public Human? login(string identifier, string password)
        {
            Human? user = _humanRepository.getByIdentifier(identifier);
            if(user == null)
            {
                return null;
            }
            string hashPassword = user.HumanPassword;
            if(Argon2.Verify(hashPassword, password))
            {
                return user;
            }
            return null;
        }
        public Human? getUser(int userId)
        {
            return _humanRepository.getById(userId);
        }
        public bool updateUser(int userId, Human user)
        {
            user.HumanPassword = Argon2.Hash(user.HumanPassword);
            return _humanRepository.update(userId, user);
        }
    }
}
