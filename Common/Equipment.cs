using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Equipment
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string Photo { get; private set; }
        [Required]
        public int Number { get; private set; }
        public EquipmentType TypeEquipment { get; private set; }
        public ModelType TypeModel { get; private set; }
        public Auditoruim Auditoruim { get; private set; }
        public User ResponsibleUser { get; private set; }
        public User TemporarilyResponsibleUser { get; private set; }
        public decimal Cost { get; private set; }
        public Route Route { get; private set; }
        public Status Status { get; private set; }
        public List<NetworkSetting> NetworkSettings { get; private set; }
        public Programm Programm { get; private set; }
        public Consumable Consumable { get; private set; }
        public string Comment { get; private set; }
    }
}