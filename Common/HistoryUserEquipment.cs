using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryUserEquipment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EquipmentId { get; set; }
        public string Date { get; set; }
    }
}