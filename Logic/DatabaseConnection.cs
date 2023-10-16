using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// A static class for asynchronously checking the status of server and local databases by invoking methods from the relevant namespaces.
    /// </summary>
    public static class DatabaseConnection
    {
        /// <summary>
        /// Asynchronously checks if a connection to the server databases exists.
        /// </summary>
        /// <returns>A task that returns true if a connection to the server databases exists, false otherwise.</returns>
        public static Task<bool> IsServerDatabasesConnectionExistAsync()
        {
            return Task.Run(() =>
            {
                return DataBaseServer.StatusChecker.IsDatabaseConnectionExist();
            });
        }

        /// <summary>
        /// Asynchronously checks if a connection to the local databases exists.
        /// </summary>
        /// <returns>A task that returns true if a connection to the local databases exists, false otherwise.</returns>
        public static Task<bool> IsLocalDatabasesConnectionExistAsync()
        {
            return Task.Run(() =>
            {
                return DataBaseLocal.StatusChecker.IsDatabaseConnectionExist();
            });
        }

        /// <summary>
        /// Asynchronously checks if the server databases exist.
        /// </summary>
        /// <returns>A task that returns true if the server databases exist, false otherwise.</returns>
        public static Task<bool> IsServerDatabasesExistAsync()
        {
            return Task.Run(() =>
            {
                return DataBaseServer.StatusChecker.IsDatabaseExist();
            });
        }

        /// <summary>
        /// Asynchronously checks if the local databases exist.
        /// </summary>
        /// <returns>A task that returns true if the local databases exist, false otherwise.</returns>
        public static Task<bool> IsLocalDatabasesExistAsync()
        {
            return Task.Run(() =>
            {
                return DataBaseLocal.StatusChecker.IsDatabaseExist();
            });
        }

        /// <summary>
        /// Asynchronously checks if a request to the server databases can be made by invoking the "IsDatabaseRequestExist" method from the "DataBaseServer.StatusChecker."
        /// </summary>
        /// <returns>A task that returns true if a request to the server databases can be made successfully, false otherwise.</returns>
        public static Task<bool> IsServerDatabasesRequestExistAsync()
        {
            return Task.Run(() =>
            {
                return DataBaseServer.StatusChecker.IsDatabaseRequestExist();
            });
        }

        /// <summary>
        /// Asynchronously checks if a request to the local databases can be made by invoking the "IsDatabaseRequestExist" method from the "DataBaseLocal.StatusChecker."
        /// </summary>
        /// <returns>A task that returns true if a request to the local databases can be made successfully, false otherwise.</returns>
        public static Task<bool> IsLocalDatabasesRequestExistAsync()
        {
            return Task.Run(() =>
            {
                return DataBaseLocal.StatusChecker.IsDatabaseRequestExist();
            });
        }
    }
}