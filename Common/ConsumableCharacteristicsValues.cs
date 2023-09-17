using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ConsumableCharacteristicsValues
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}