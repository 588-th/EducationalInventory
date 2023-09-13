using Common;
using SQLite.Net;

namespace DataBaseLocal
{
    public class CreateDataBase
    {
        public void CreateTables()
        {
            SQLiteConnection connection = Connection.GetConnection();

            var tableTypes = new[]
            {
                typeof(Auditoruim),
                typeof(Consumable),
                typeof(ConsumableCharacteristics),
                typeof(ConsumableCharacteristicsValues),
                typeof(ConsumableType),
                typeof(Developer),
                typeof(Equipment),
                typeof(EquipmentType),
                typeof(HistoryAudienceEquipment),
                typeof(HistoryUserConsumable),
                typeof(HistoryUserEquipment),
                typeof(Inventory),
                typeof(ModelType),
                typeof(NetworkSetting),
                typeof(Programm),
                typeof(Route),
                typeof(Status),
                typeof(User)
            };

            foreach (var tableType in tableTypes)
            {
                connection.CreateTable(tableType);
            }

            Connection.SetConnection(connection);
        }
    }
}