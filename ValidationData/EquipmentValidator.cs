namespace ValidationData
{
    /// <summary>
    /// Provides methods for validating equipment-related data, such as inventory numbers and costs.
    /// </summary>
    public static class EquipmentValidator
    {
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