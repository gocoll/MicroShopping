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
        UserCreationResults CreateUser(User user);
        bool ValidateCredentials(string email, string password);
        void SaveChanges();
    }
}
