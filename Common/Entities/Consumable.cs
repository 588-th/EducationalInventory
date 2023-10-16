using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("Consumables")]
    public class Consumable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReceiptDate { get; set; }
        public string Photo { get; set; }
        public int Count { get; set; }
        public int ResponsibleUserId { get; set; }
        public int TemporarilyResponsibleUserId { get; set; }
        public int ConsumableTypeId { get; set; }
    }
}