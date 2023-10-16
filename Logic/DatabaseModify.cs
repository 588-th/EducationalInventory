using Common;

namespace Logic
{
    /// <summary>
    /// A static class for modifying entities in both server and local databases based on the specified action (Add, Update, Delete). 
    /// </summary>
    public static class DatabaseModify
    {
        /// <summary>
        /// Enumerates actions for modifying entities, including Add, Update, and Delete.
        /// </summary>
        public enum Action
        {
            Add,
            Update,
            Delete,
        }

        /// <summary>
        /// Modifies an entity in both server and local databases based on the specified action.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be modified.</typeparam>
        /// <param name="entity">The entity to be modified.</param>
        /// <param name="action">The action to be performed (Add, Update, or Delete).</param>
        /// <returns>A tuple containing a boolean result (true if successful) and an optional error message.</returns>
        public static (bool result, string error) ModifyEntity<TEntity>(TEntity entity, Action action) where TEntity : class
        {
            if (action == Action.Delete)
            {
                DataBaseServer.EntityModify.DeleteEntity(entity);
                DataBaseLocal.EntityModify.DeleteEntity(entity);
                return (true, "");
            }

            (bool result, string error) = ValidateEntity(entity);
            if (!result)
            {
                return (result, error);
            }

            if (action == Action.Add)
            {
                DataBaseServer.EntityModify.AddEntity(entity);
                DataBaseLocal.EntityModify.AddEntity(entity);
            }
            else if (action == Action.Update)
            {
                DataBaseServer.EntityModify.UpdateEntity(entity);
                DataBaseLocal.EntityModify.UpdateEntity(entity);
            }

            return (true, "");
        }

        /// <summary>
        /// Validates the specified entity based on its type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be validated.</typeparam>
        /// <param name="entity">The entity to be validated.</param>
        /// <returns>A tuple containing a boolean result (true if valid) and an optional error message.</returns>
        private static (bool result, string error) ValidateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            switch (entity)
            {
                case User user:
                    return ValidationData.UserValidator.ValidateUser(user);
                case Equipment equipment:
                    return ValidationData.EquipmentValidator.ValidateEquipment(equipment);
                case NetworkSetting networkSetting:
                    return ValidationData.NetworkSettingValidator.ValidateNetworkSetting(networkSetting);
                case Consumable consumable:
                    return ValidationData.ConsumableValidator.ValidateConsumable(consumable);
                default:
                    return (true, "");
            }
        }
    }
}