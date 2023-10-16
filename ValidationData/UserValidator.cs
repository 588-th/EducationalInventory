using Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ValidationData
{
    /// <summary>
    /// Provides methods for validating user-related data, such as login credentials, names, roles, contact information, and addresses.
    /// </summary>
    public static class UserValidator
    {
        public static (bool result, string error) ValidateUser(User user)
        {
            Dictionary<Func<string, bool>, string> validationChecks = new Dictionary<Func<string, bool>, string>
            {
                { ValidateLogin, "Invalid login format" },
                { ValidatePassword, "Invalid password format" },
                { ValidateFirstName, "Invalid first name format" },
                { ValidateSecondName, "Invalid last name format" },
                { ValidateMiddleName, "Invalid middle name format" },
                { ValidateRole, "Invalid role format" },
                { ValidatePhoneNumber, "Invalid phone number format" },
                { ValidateEmail, "Invalid email format" },
                { ValidateAddress, "Invalid address format" }
            };

            foreach (var validationCheck in validationChecks)
            {
                bool isValid = validationCheck.Key.Invoke(GetUserField(user, validationCheck.Key));
                if (!isValid)
                {
                    return (false, validationCheck.Value);
                }
            }

            return (true, "");
        }

        private static string GetUserField(User user, Func<string, bool> validationFunction)
        {
            if (validationFunction == ValidateLogin) return user.Login;
            if (validationFunction == ValidatePassword) return user.Password;
            if (validationFunction == ValidateFirstName) return user.FirstName;
            if (validationFunction == ValidateSecondName) return user.SecondName;
            if (validationFunction == ValidateMiddleName) return user.MiddleName;
            if (validationFunction == ValidateRole) return user.Role;
            if (validationFunction == ValidatePhoneNumber) return user.Phone;
            if (validationFunction == ValidateEmail) return user.Email;
            if (validationFunction == ValidateAddress) return user.Address;

            return string.Empty;
        }

        /// <summary>
        /// Validates a user login for length and allowed characters.
        /// </summary>
        /// <param name="login">The user login to validate.</param>
        /// <returns>True if the login is valid, otherwise false.</returns>
        public static bool ValidateLogin(string login)
        {
            string lengthPattern = @"^.{4,15}$";
            string lettersAndDigitsPattern = @"^[a-zA-Z0-9]+$";

            if (!Regex.IsMatch(login, lengthPattern))
            {
                return false;
            }

            if (!Regex.IsMatch(login, lettersAndDigitsPattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user password for length, capital letters, special symbols, and numbers.
        /// </summary>
        /// <param name="password">The user password to validate.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        public static bool ValidatePassword(string password)
        {
            string lengthPattern = @"^.{8,32}$";
            string capitalLetterPattern = @"[A-Z]";
            string specialSymbolPattern = @"[\W_]";
            string numberPattern = @"\d";

            if (!Regex.IsMatch(password, lengthPattern))
            {
                return false;
            }

            if (!Regex.IsMatch(password, capitalLetterPattern))
            {
                return false;
            }

            if (!Regex.IsMatch(password, specialSymbolPattern))
            {
                return false;
            }

            if (!Regex.IsMatch(password, numberPattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user's first name for proper formatting.
        /// </summary>
        /// <param name="firstName">The user's first name to validate.</param>
        /// <returns>True if the first name is valid, otherwise false.</returns>
        public static bool ValidateFirstName(string firstName)
        {
            string firstNamePattern = @"^[A-Z][a-z]*$";

            if (!Regex.IsMatch(firstName, firstNamePattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user's second name for proper formatting.
        /// </summary>
        /// <param name="secondName">The user's second name to validate.</param>
        /// <returns>True if the second name is valid, otherwise false.</returns>
        public static bool ValidateSecondName(string secondName)
        {
            string secondNamePattern = @"^[A-Z][a-z]*$";

            if (!Regex.IsMatch(secondName, secondNamePattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user's middle name for proper formatting.
        /// </summary>
        /// <param name="middleName">The user's middle name to validate.</param>
        /// <returns>True if the middle name is valid, otherwise false.</returns>
        public static bool ValidateMiddleName(string middleName)
        {
            string middleNamePattern = @"^[A-Z][a-z]*$";

            if (!Regex.IsMatch(middleName, middleNamePattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user's role to ensure it's either "Administrator" or "Employee".
        /// </summary>
        /// <param name="role">The user's role to validate.</param>
        /// <returns>True if the role is valid, otherwise false.</returns>
        public static bool ValidateRole(string role)
        {
            if (role != "Administrator" && role != "Employee")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user's phone number for length and starting with the digit "8".
        /// </summary>
        /// <param name="phoneNumber">The user's phone number to validate.</param>
        /// <returns>True if the phone number is valid, otherwise false.</returns>
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber == "")
            {
                return false;
            }

            if (phoneNumber.Length != 12)
            {
                return false;
            }

            if (phoneNumber[0] != '+')
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user's email address for proper formatting.
        /// </summary>
        /// <param name="email">The user's email address to validate.</param>
        /// <returns>True if the email address is valid, otherwise false.</returns>
        public static bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a user's address by splitting it into parts and checking for proper formatting.
        /// </summary>
        /// <param name="address">The user's address to validate.</param>
        /// <returns>True if the address is valid, otherwise false.</returns>
        public static bool ValidateAddress(string address)
        {
            string[] parts = address.Split(' ');

            if (parts.Length < 3)
            {
                return false;
            }

            string city = parts[0].Trim();
            string street = parts[1].Trim();
            string houseNumber = parts[2].Trim();

            if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(houseNumber))
            {
                return false;
            }

            return true;
        }
    }
}