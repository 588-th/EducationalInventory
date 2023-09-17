using Common;
using System.Data.Entity;

namespace DataBaseLocal
{
    public class LocalInventoryContext : DbContext
    {
        public LocalInventoryContext() : base("LocalConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LocalInventoryContext>());
        }

        public DbSet<Audience> Audiences { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<ConsumableCharacteristics> ConsumablesCharacteristics { get; set; }
        public DbSet<ConsumableCharacteristicsValues> ConsumablesCharacteristicsValues { get; set; }
        public DbSet<ConsumableType> ConsumablesType { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentsType { get; set; }
        public DbSet<HistoryAudienceEquipment> HistoryAudienceEquipments { get; set; }
        public DbSet<HistoryUserConsumable> HistoryUserConsumables { get; set; }
        public DbSet<HistoryUserEquipment> HistoryUserEquipments { get; set; }
        public DbSet<Inventory> Inventoryes { get; set; }
        public DbSet<NetworkSetting> NetworkSettings { get; set; }
        public DbSet<Programm> Programms { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ModelType> ModelsType { get; set; }
        public DbSet<User> Users { get; set; }
    }
}