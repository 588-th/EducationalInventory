using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBaseServer
{
    public static class Controll
    {
        public static bool IsDataBaseExist()
        {
            return Database.Exists("DefaultConnection");
        }

        public static List<object> GetItemsList(string entityName)
        {
            using (var dbContext = new InventoryContext())
            {
                // Find the DbSet corresponding to the entity name
                var entityType = Type.GetType("DataBase." + entityName);
                if (entityType == null)
                {
                    throw new ArgumentException("Invalid entity name.");
                }

                // Get the DbSet using reflection
                var dbSet = dbContext.Set(entityType);

                // Query the database to retrieve items
                var items = dbSet.Cast<object>().ToList();

                return items;
            }
        }
    }
}