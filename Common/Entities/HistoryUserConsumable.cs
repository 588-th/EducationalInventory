using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("HistoryUserConsumables")]
    public class HistoryUserConsumable
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ConsumableId { get; set; }
        public string Date { get; set; }
    }
}