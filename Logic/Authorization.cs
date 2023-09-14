namespace Logic
{
    public static class Authorization
    {
        public static (bool, string, string) AuthorizeUser(string login, string password)
        {
            bool isLoginValid = ValidationData.UserValidator.ValidateLogin(login);
            bool isPasswordValid = ValidationData.UserValidator.ValidatePassword(password);

            if (!isLoginValid)
            {
                return (false, "Login is not valid", "");
            }

            if (!isPasswordValid)
            {
                return (false, "Password is not valid", "");
            }

            return (true, "", "Administrator");
        }
    }
}