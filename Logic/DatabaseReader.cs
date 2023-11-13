using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// A static class for reading data from server databases and performing asynchronous data retrieval operations.
    /// </summary>
    public static class DatabaseReader
    {
        /// <summary>
        /// Retrieves a list of entities of a specified type from the server databases.
        /// </summary>
        /// <param name="entityName">The name of the entity type to retrieve.</param>
        /// <returns>A list of objects representing the retrieved entities.</returns>
        /// <exception cref="ArgumentException">Thrown if the provided entity name is invalid.</exception>
        public static List<object> GetEntityList(string entityName)
        {
            Type entityType = Entities.GetType(entityName) ?? throw new ArgumentException("Invalid entity name");

            var methodInfo = typeof(DataBaseServer.EntityReader).GetMethod("GetItemList");
            var genericMethod = methodInfo.MakeGenericMethod(entityType);

            var itemsList = ((IEnumerable)genericMethod.Invoke(null, null)).OfType<object>().ToList();

            return itemsList;
        }

        /// <summary>
        /// Retrieves an entity of a specified type from the server databases by its ID.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be retrieved.</typeparam>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>The retrieved entity or null if not found.</returns>
        public static TEntity GetEntity<TEntity>(int id) where TEntity : class
        {
            return DataBaseServer.EntityReader.GetItem<TEntity>(id);
        }

        /// <summary>
        /// Asynchronously retrieves a list of entities of a specified type from the server databases.
        /// </summary>
        /// <param name="entityName">The name of the entity type to retrieve.</param>
        /// <returns>A task that returns a list of objects representing the retrieved entities.</returns>
        /// <exception cref="ArgumentException">Thrown if the provided entity name is invalid.</exception>
        public static Task<List<object>> GetEntityListAsync(string entityName)
        {
            return Task.Run(() =>
            {
                Type entityType = Entities.GetType(entityName) ?? throw new ArgumentException("Invalid entity name");

                var methodInfo = typeof(DataBaseServer.EntityReader).GetMethod("GetItemList");
                var genericMethod = methodInfo.MakeGenericMethod(entityType);

                var itemsList = ((IEnumerable)genericMethod.Invoke(null, null)).OfType<object>().ToList();

                return itemsList;
            });
        }

        /// <summary>
        /// Asynchronously retrieves an entity of a specified type from the server databases by its ID.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be retrieved.</typeparam>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>A task that returns the retrieved entity or null if not found.</returns>
        public static Task<TEntity> GetEntityAsync<TEntity>(int id) where TEntity : class
        {
            return Task.Run(() =>
            {
                return DataBaseServer.EntityReader.GetItem<TEntity>(id);
            });
        }

        public static User GetUser(string login)
        {
            return DataBaseServer.EntityReader.GetUser(login);
        }
    }
}