using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Inventory
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string StartDate { get; private set; }
        public string EndDate { get; private set; }
        public int EquipmentId { get; private set; }
        public int UserId { get; private set; }
    }
}