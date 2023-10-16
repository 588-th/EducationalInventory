using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("ConsumablesCharacteristicsValues")]
    public class ConsumableCharacteristicsValues
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}