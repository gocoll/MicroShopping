using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Concrete;

namespace MicroShopping.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> FindAllUsers();
        User FindUserByEmail(string email);
        User FindUserById(int userId);
        User FindUserByNickname(string nickname);
        UserCreationResults CreateUser(User user);

        string FindRoleForUserById(int userId);
        bool ValidateCredentials(string email, string password);
        void SaveChanges();
    }
}
