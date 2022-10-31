using System.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DatingApp.API.Data.Entities;

namespace DatingApp.API.Data.Seed
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            if (context.AppUsers.Any())
            {
                return;
            }

            var usersText = System.IO.File.ReadAllText("Data/Seed/users.json");
            var users = JsonSerializer.Deserialize<List<User>>(usersText);
            if (users == null)
            {
                return;
            }

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHashed = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@$$w0rd1"));
                user.PasswordSalt = hmac.Key;
                user.CreatedAt = DateTime.Now;
                context.AppUsers.Add(user);
            }
            context.SaveChanges();
        }
    }
}