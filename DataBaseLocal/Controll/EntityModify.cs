using System.Data.Entity;

namespace DataBaseLocal
{
    /// <summary>
    /// A static class for common database operations such as adding, updating, and deleting entities using Entity Framework, in the context of the "LocalInventoryContext."
    /// </summary>
    public static class EntityModify
    {
        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be added.</typeparam>
        /// <param name="entity">The entity to be added.</param>
        public static void AddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = new LocalInventoryContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be updated.</typeparam>
        /// <param name="entity">The entity to be updated.</param>
        public static void UpdateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = new LocalInventoryContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be deleted.</typeparam>
        /// <param name="entity">The entity to be deleted.</param>
        public static void DeleteEntity<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = new LocalInventoryContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}