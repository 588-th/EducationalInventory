using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBaseServer
{
    /// <summary>
    /// A static class for reading data from the database using Entity Framework, specifically in the context of the "ServerInventoryContext."
    /// </summary>
    public static class EntityReader
    {
        /// <summary>
        /// Retrieves a list of items from the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of item to be retrieved.</typeparam>
        /// <returns>A list of items of the specified type.</returns>
        public static List<TEntity> GetItemList<TEntity>() where TEntity : class
        {
            using (var dbContext = new ServerInventoryContext())
            {
                return dbContext.Set<TEntity>().ToList();
            }
        }

        /// <summary>
        /// Retrieves a single item from the database based on its ID.
        /// </summary>
        /// <typeparam name="TEntity">The type of item to be retrieved.</typeparam>
        /// <param name="id">The unique identifier of the item to be retrieved.</param>
        /// <returns>The item with the specified ID, or null if not found.</returns>
        public static TEntity GetItem<TEntity>(int id) where TEntity : class
        {
            using (var dbContext = new ServerInventoryContext())
            {
                DbSet<TEntity> entitySet = dbContext.Set<TEntity>();
                return entitySet.Find(id);
            }
        }
    }
}