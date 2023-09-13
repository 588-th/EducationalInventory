using System.Data.Entity;

namespace DataBase
{
    public static class DataRepository
    {
        private static string connectionString;

        private static Context context = new Context();

        /// <summary>
        /// Checking database status
        /// </summary>
        /// <returns>If available true, otherwise false</returns>
        public static bool IsDataBaseExist()
        {
            return Database.Exists(connectionString);
        }
    }
}