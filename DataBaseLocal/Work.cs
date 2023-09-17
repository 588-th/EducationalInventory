using Common;
using System;
using System.Linq;

namespace DataBaseLocal
{
    public static class Work
    {
        public static void Main()
        {
            using (var dbContext = new LocalInventoryContext())
            {
                var newUser = new User
                {
                    Login = "johndoe",
                    Password = "password123",
                    Role = "User",
                    Email = "johndoe@example.com",
                    FirstName = "John",
                    SecondName = "Doe",
                    MiddleName = "M.",
                    Phone = "123-456-7890",
                    Address = "123 Main St"
                };

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                var users = dbContext.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"User ID: {user.Id}, Name: {user.FirstName} {user.SecondName}, Email: {user.Email}");
                }
            }
        }
    }
}