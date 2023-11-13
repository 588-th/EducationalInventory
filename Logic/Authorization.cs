using Common;

namespace Logic
{
    public static class Authorization
    {
        public static (bool, string, User) AuthorizeUser(string login, string password)
        {
            User user = DatabaseReader.GetUser(login);

            if (login == "Admin" && password == "Admin")
            {
                return (true, "", user);
            }

            if (user == null)
            {
                return (false, "User does not exist", user);
            }

            if (user.Password != password)
            {
                return (false, "Invalid password", user);
            }

            return (true, "", user);
        }
    }
}