using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data.Entities;

namespace DatingApp.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            this._context = context;
        }

        public User GetUserById(int id)
        {
            return this._context.AppUsers.FirstOrDefault(user => user.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return this._context.AppUsers.FirstOrDefault(user => user.Username == username);
        }

        public List<User> GetUsers()
        {
            return this._context.AppUsers.ToList();
        }

        public void InsertNewUser(User user)
        {
            _context.AppUsers.Add(user);
        }

        public void UpdateUser(User user)
        {
            _context.AppUsers.Update(user);
        }

        public void DeleteUser(User user)
        {
            _context.AppUsers.Remove(user);
        }

        public bool IsSaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}