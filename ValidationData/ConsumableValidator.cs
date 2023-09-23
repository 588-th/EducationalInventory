using Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ValidationData
{
    /// <summary>
    /// Provides methods for validating consumable data such as receipt dates and counts.
    /// </summary>
    public static class ConsumableValidator
    {
        public static (bool result, string error) ValidateConsumable(Consumable consumable)
        {
            Dictionary<Func<string, bool>, string> validationChecks = new Dictionary<Func<string, bool>, string>
            {
                { ValidateReceiptDate, "Invalid inventory number format" },
                { ValidateCount, "Invalid cost format" }
            };

            foreach (var validationCheck in validationChecks)
            {
                bool isValid = validationCheck.Key.Invoke(GetConsumableField(consumable, validationCheck.Key));
                if (!isValid)
                {
                    return (false, validationCheck.Value);
                }
            }

            return (true, "");
        }

        private static string GetConsumableField(Consumable consumable, Func<string, bool> validationFunction)
        {
            if (validationFunction == ValidateReceiptDate) return consumable.ReceiptDate.ToString();
            if (validationFunction == ValidateCount) return consumable.Count.ToString();

            return string.Empty;
        }

        /// <summary>
        /// Validates a receipt date string in the format "dd.MM.yyyy".
        /// </summary>
        /// <param name="receiptDate">The receipt date to validate.</param>
        /// <returns>True if the receipt date is valid, otherwise false.</returns>
        public static bool ValidateReceiptDate(string receiptDate)
        {
            string dateFormat = "dd.MM.yyyy";

            if (!DateTime.TryParseExact(receiptDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a count string to ensure it represents an integer value.
        /// </summary>
        /// <param name="count">The count to validate as a string.</param>
        /// <returns>True if the count is a valid integer, otherwise false.</returns>
        public static bool ValidateCount(string count)
        {
            if (int.TryParse(count, out _))
            {
                return false;
            }

            return true;
        }
    }
}