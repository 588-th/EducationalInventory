using System;
using System.Linq;

namespace DataBaseLocal
{
    /// <summary>
    /// A static class for checking the status of the database, including its connection and existence, in the context of the "LocalInventoryContext."
    /// </summary>
    public static class StatusChecker
    {
        /// <summary>
        /// Checks if a connection to the database exists.
        /// </summary>
        /// <returns>True if a database connection exists, false otherwise.</returns>
        public static bool IsDatabaseConnectionExist()
        {
            try
            {
                using (var dbContext = new LocalInventoryContext())
                {
                    dbContext.Database.Connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the database exists.
        /// </summary>
        /// <returns>True if the database exists, false otherwise.</returns>
        public static bool IsDatabaseExist()
        {
            try
            {
                using (var dbContext = new LocalInventoryContext())
                {
                    return dbContext.Database.Exists();
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a request to the server database can be made by attempting to retrieve the first user.
        /// </summary>
        /// <returns>True if a request to the server database can be made successfully, false otherwise.</returns>
        public static bool IsDatabaseRequestExist()
        {
            try
            {
                using (var dbContext = new LocalInventoryContext())
                {
                    dbContext.Users.FirstOrDefault();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}