using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public static class DataBaseLogic
    {
        public enum Action
        {
            Add,
            Update,
            Delete,
        }

        public static void IsDataBasesExist()
        {
            DataBaseServer.Controll.IsDataBaseExist();
            DataBaseLocal.Controll.IsDataBaseExist();
        }

        public static List<object> GetEntityList(string entityName)
        {
            Type entityType = Entities.GetType(entityName) ?? throw new ArgumentException("Invalid entity name");

            var methodInfo = typeof(DataBaseServer.Controll).GetMethod("GetItemList");
            var genericMethod = methodInfo.MakeGenericMethod(entityType);

            var itemsList = ((IEnumerable)genericMethod.Invoke(null, null))
                            .OfType<object>()
                            .ToList();

            return itemsList;
        }

        public static TEntity GetEntity<TEntity>(int id) where TEntity : class
        {
            return DataBaseServer.Controll.GetItem<TEntity>(id);
        }

        public static (bool result, string error) ModifyUser(User user, Action action)
        {
            if (action == Action.Delete)
            {
                DataBaseServer.Controll.DeleteEntity(user);
                DataBaseLocal.Controll.DeleteEntity(user);
                return (true, "");
            }

            (bool result, string error) = ValidationData.UserValidator.ValidateUser(user);
            if (!result)
            {
                return (result, error);
            }

            if (action == Action.Add)
            {
                DataBaseServer.Controll.AddEntity(user);
                DataBaseLocal.Controll.AddEntity(user);
            }
            else if (action == Action.Update)
            {
                DataBaseServer.Controll.UpdateEntity(user);
                DataBaseLocal.Controll.UpdateEntity(user);
            }

            return (true, "");
        }
    }
}