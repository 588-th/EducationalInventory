using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataBaseLocal
{
    /// <summary>
    /// A static class for reading data from the database using Entity Framework, specifically in the context of the "LocalInventoryContext."
    /// </summary>
    public static class EntityReader
    {
        /// <summary>
        /// Retrieves a list of entities from the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be retrieved.</typeparam>
        /// <returns>A list of entities of the specified type.</returns>
        public static List<TEntity> GetEntityList<TEntity>() where TEntity : class
        {
            using (var dbContext = new LocalInventoryContext())
            {
                return dbContext.Set<TEntity>().ToList();
            }
        }

        /// <summary>
        /// Retrieves a single entity from the database based on its ID.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be retrieved.</typeparam>
        /// <param name="id">The unique identifier of the entity to be retrieved.</param>
        /// <returns>The entity with the specified ID, or null if not found.</returns>
        public static TEntity GetEntity<TEntity>(int id) where TEntity : class
        {
            using (var dbContext = new LocalInventoryContext())
            {
                DbSet<TEntity> entitySet = dbContext.Set<TEntity>();
                return entitySet.Find(id);
            }
        }
    }
}