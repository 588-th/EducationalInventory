using System;
using System.Collections.Generic;

namespace Logic
{
    public static class DataBaseLogic
    {
        public static void StartDataBase()
        {
            DataBaseLocal.Work.Main();
        }

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
    }
}
