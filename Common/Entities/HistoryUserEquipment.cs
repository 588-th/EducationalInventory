using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("HistoryUserEquipments")]
    public class HistoryUserEquipment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EquipmentId { get; set; }
        public string Date { get; set; }
    }
}