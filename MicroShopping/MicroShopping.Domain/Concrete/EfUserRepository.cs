using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Abstract;

namespace MicroShopping.Domain.Concrete
{
    public class EfUserRepository : IUserRepository, IDisposable
    {
        private MicroshoppingEntities db = new MicroshoppingEntities();

        public IQueryable<User> FindAllUsers() 
        {
            return db.Users;
        }

        public User FindUserByEmail(string email)
        {
            return db.Users.SingleOrDefault(u => u.Email == email);
        }

        public User FindUserById(int userId)
        {
            return db.Users.SingleOrDefault(u => u.UserId == userId);
        }

        public User FindUserByNickname(string nickname)
        {
            return db.Users.SingleOrDefault(u => u.Nickname == nickname);
        }

        public UserCreationResults CreateUser(User user)
        {
            var email = db.Users.SingleOrDefault(u => u.Email == user.Email);
            if (email != null)
                return UserCreationResults.EmailAlreadyExists;

            var nickname = db.Users.SingleOrDefault(u => u.Nickname == user.Nickname);
            if (nickname != null)
                return UserCreationResults.NicknameAlreadyExists;

            var carnet = db.Users.SingleOrDefault(u => u.Carnet == user.Carnet);
            if (carnet != null)
                return UserCreationResults.CarnetAlreadyExists;

            try
            {
                db.Users.AddObject(user);
                db.SaveChanges();

                return UserCreationResults.Ok;
            }
            catch (Exception)
            {
                return UserCreationResults.UnknownError;
            }
        }

        public string FindRoleForUserById(int userId)
        {
            var user = db.Users.SingleOrDefault(u => u.UserId == userId);
            if (user != null)
                return user.UserRole.Name;
            
            return String.Empty;
        }

        public bool ValidateCredentials(string email, string password)
        {
            var user = db.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
            {
                var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

                if (valid)
                {
                    user.LastDateLogin = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UserHasDashboardRights(string email)
        {
            var user = db.Users.SingleOrDefault(u => u.Email == email);
            switch (user.UserRoleId)
            {
                case 1:
                    return false;
                case 2:
                    return true;
                case 3:
                    return true;
                case 4:
                    return true;
                case 5:
                    return true;
                case 6:
                    return true;
                default:
                    return true;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }    
    }

    public enum UserCreationResults
    {
        Ok,
        UsernameExists,
        EmailAlreadyExists,
        NicknameAlreadyExists,
        CarnetAlreadyExists,
        UnknownError
    }
}
