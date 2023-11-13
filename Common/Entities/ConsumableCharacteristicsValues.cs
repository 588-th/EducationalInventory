using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("ConsumableCharacteristicsValues")]
    public class ConsumableCharacteristicsValues
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}