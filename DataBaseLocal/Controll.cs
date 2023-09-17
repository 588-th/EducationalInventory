using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBaseLocal
{
    public static class Controll
    {
        public static List<TEntity> GetItemList<TEntity>() where TEntity : class
        {
            using (var dbContext = new LocalInventoryContext())
            {
                return dbContext.Set<TEntity>().ToList();
            }
        }

        public static TEntity GetItem<TEntity>(int id) where TEntity : class
        {
            using (var dbContext = new LocalInventoryContext())
            {
                DbSet<TEntity> entitySet = dbContext.Set<TEntity>();
                return entitySet.Find(id);
            }
        }
    }
}