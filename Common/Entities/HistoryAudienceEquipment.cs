using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("HistoryAudienceEquipments")]
    public class HistoryAudienceEquipment
    {
        [Key]
        public int Id { get; set; }
        public int AudienceId { get; set; }
        public int EquipmentId { get; set; }
        public string Date { get; set; }
    }
}