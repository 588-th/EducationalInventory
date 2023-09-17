using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ConsumableCharacteristics
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConsumableCharacteristicsValuesId { get; set; }
    }
}