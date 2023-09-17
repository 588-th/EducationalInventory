using System;
using System.Collections.Generic;

namespace Logic
{
    public static class DataBaseLogic
    {
        public static List<object> GetEntityList(string entityName)
        {
            Type entityType = Common.Entities.GetType(entityName);

            if (entityType == null)
            {
                throw new ArgumentException("Invalid entity name.");
            }

            var methodInfo = typeof(DataBaseLocal.Controll).GetMethod("GetItemsList");
            var genericMethod = methodInfo.MakeGenericMethod(entityType);
            var itemsList = (List<object>)genericMethod.Invoke(null, null);

            return itemsList;
        }

        public static TEntity GetEntity<TEntity>(int id) where TEntity : class
        {
            return DataBaseLocal.Controll.GetItem<TEntity>(id);
        }
    }
}