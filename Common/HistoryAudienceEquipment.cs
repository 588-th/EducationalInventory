using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryAudienceEquipment
    {
        [Key]
        public int Id { get; private set; }
        public int AudienceId { get; private set; }
        public int EquipmentId { get; private set; }
        public string Date { get; private set; }
    }
}