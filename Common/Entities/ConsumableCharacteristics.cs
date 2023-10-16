using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("ConsumablesCharacteristics")]
    public class ConsumableCharacteristics
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConsumableCharacteristicsValuesId { get; set; }
    }
}