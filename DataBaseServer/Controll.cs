using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBaseServer
{
    public static class Controll
    {
        public static bool IsDataBaseExist()
        {
            using (var dbContext = new ServerInventoryContext())
            {
                return Database.Exists("ServerConnection");
            }
        }

        public static List<TEntity> GetItemList<TEntity>() where TEntity : class
        {
            using (var dbContext = new ServerInventoryContext())
            {
                return dbContext.Set<TEntity>().ToList();
            }
        }

        public static TEntity GetItem<TEntity>(int id) where TEntity : class
        {
            using (var dbContext = new ServerInventoryContext())
            {
                DbSet<TEntity> entitySet = dbContext.Set<TEntity>();
                return entitySet.Find(id);
            }
        }

        public static void AddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = new ServerInventoryContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public static void UpdateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = new ServerInventoryContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void DeleteEntity<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = new ServerInventoryContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}