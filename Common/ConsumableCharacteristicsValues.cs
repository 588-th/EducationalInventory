using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ConsumableCharacteristicsValues
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}