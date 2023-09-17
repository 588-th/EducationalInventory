using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryAudienceEquipment
    {
        [Key]
        public int Id { get; set; }
        public int AudienceId { get; set; }
        public int EquipmentId { get; set; }
        public string Date { get; set; }
    }
}