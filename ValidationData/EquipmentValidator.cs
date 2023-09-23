using Common;
using System;
using System.Collections.Generic;

namespace ValidationData
{
    /// <summary>
    /// Provides methods for validating equipment-related data, such as inventory numbers and costs.
    /// </summary>
    public static class EquipmentValidator
    {
        public static (bool result, string error) ValidateEquipment(Equipment equipment)
        {
            Dictionary<Func<string, bool>, string> validationChecks = new Dictionary<Func<string, bool>, string>
            {
                { ValidateInventoryNumber, "Invalid inventory number format" },
                { ValidateCost, "Invalid cost format" }
            };

            foreach (var validationCheck in validationChecks)
            {
                bool isValid = validationCheck.Key.Invoke(GetEquipmentField(equipment, validationCheck.Key));
                if (!isValid)
                {
                    return (false, validationCheck.Value);
                }
            }

            return (true, "");
        }

        private static string GetEquipmentField(Equipment equipment, Func<string, bool> validationFunction)
        {
            if (validationFunction == ValidateInventoryNumber) return equipment.Number.ToString();
            if (validationFunction == ValidateCost) return equipment.Cost.ToString();

            return string.Empty;
        }

        /// <summary>
        /// Validates an inventory number to ensure it represents an integer value.
        /// </summary>
        /// <param name="inventoryNumber">The inventory number to validate as a string.</param>
        /// <returns>True if the inventory number is a valid integer, otherwise false.</returns>
        public static bool ValidateInventoryNumber(string inventoryNumber)
        {
            if (!int.TryParse(inventoryNumber, out _))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates a cost value to ensure it represents an integer value.
        /// </summary>
        /// <param name="cost">The cost to validate as a string.</param>
        /// <returns>True if the cost is a valid integer, otherwise false.</returns>
        public static bool ValidateCost(string cost)
        {
            if (!int.TryParse(cost, out _))
            {
                return false;
            }

            return true;
        }
    }
}