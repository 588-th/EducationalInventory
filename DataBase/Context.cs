using Common;
using System.Data.Entity;

namespace DataBase
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection") { }

        public DbSet<Auditoruim> Auditoruims { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<CharacteristicsConsumable> CharacteristicsConsumables { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<HistoryAudienceEquipment> HistoryAudienceEquipments { get; set; }
        public DbSet<HistoryUserConsumable> HistoryUserConsumables { get; set; }
        public DbSet<HistoryUserEquipment> HistoryUserEquipments { get; set; }
        public DbSet<Inventory> Inventoryes { get; set; }
        public DbSet<NetworkSetting> NetworkSettings { get; set; }
        public DbSet<Programm> Programms { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TypeEquipment> TypeEquipments { get; set; }
        public DbSet<TypeModel> TypeModels { get; set; }
        public DbSet<User> Users { get; set; }
    }
}