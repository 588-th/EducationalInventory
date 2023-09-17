using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int EquipmentId { get; set; }
        public int UserId { get; set; }
    }
}