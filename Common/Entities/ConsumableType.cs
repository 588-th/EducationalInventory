using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("ConsumableTypes")]
    public class ConsumableType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConsumableCharacteristicsId { get; set; }
    }
}