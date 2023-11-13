using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("HistoryUserConsumables")]
    public class HistoryUserConsumable
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Consumable { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
    }
}