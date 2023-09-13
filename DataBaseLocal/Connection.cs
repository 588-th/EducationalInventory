using SQLite.Net;
using SQLite.Net.Interop;
using System;

namespace DataBaseLocal
{
    /// <summary>
    /// Provides methods for managing the SQLite database connection.
    /// </summary>
    public static class Connection
    {
        private static ISQLitePlatform _platform;
        private static string _databasePath;
        private static SQLiteConnection _sqliteConnection;

        /// <summary>
        /// Initializes the database connection parameters.
        /// </summary>
        /// <param name="platform">The SQLite platform implementation.</param>
        /// <param name="databasePath">The path to the SQLite database file.</param>
        public static void Initialize(ISQLitePlatform platform, string databasePath)
        {
            _platform = platform;
            _databasePath = databasePath;
        }

        /// <summary>
        /// Establishes a connection to the database.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the database connection parameters have not been initialized. Call Initialize method first.</exception>
        public static void ConnectToDataBase()
        {
            if (_platform == null || _databasePath == null)
            {
                throw new InvalidOperationException("Database connection parameters have not been initialized. Call Initialize method first.");
            }

            if (_sqliteConnection == null)
            {
                _sqliteConnection = new SQLiteConnection(_platform, _databasePath);
            }
        }

        /// <summary>
        /// Disconnects from the database and disposes of the connection.
        /// </summary>
        public static void DisconnectToDataBase()
        {
            if (_sqliteConnection != null)
            {
                _sqliteConnection.Close();
                _sqliteConnection.Dispose();
                _sqliteConnection = null;
            }
        }

        /// <summary>
        /// Retrieves the established SQLiteConnection.
        /// </summary>
        /// <returns>The established SQLiteConnection.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the database connection has not been established. Call ConnectToDataBase or SetConnection method first.</exception>
        public static SQLiteConnection GetConnection()
        {
            if (_sqliteConnection == null)
            {
                throw new InvalidOperationException("Database connection has not been established. Call ConnectToDataBase or SetConnection method first.");
            }

            return _sqliteConnection;
        }

        /// <summary>
        /// Sets the database connection to a pre-existing SQLiteConnection.
        /// </summary>
        /// <param name="connection">The pre-existing SQLiteConnection to set.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided connection is null.</exception>
        public static void SetConnection(SQLiteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection), "Connection cannot be null.");
            }

            _sqliteConnection = connection;
        }
    }
}