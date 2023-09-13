namespace Logic
{
    class Authorization
    {
        private void InitEvents()
        {

        }

        private void AuthorizeUser(string login, string password)
        {
            bool isLoginValid = ValidationData.UserValidator.ValidateLogin(login);
            bool isPasswordValid = ValidationData.UserValidator.ValidatePassword(password);

            if (isLoginValid && isPasswordValid)
            {
                var a = ProjectWindows.adminWindow;
            }
        }
    }
}