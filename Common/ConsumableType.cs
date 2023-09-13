using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ConsumableType
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<ConsumableCharacteristics> ConsumableCharacteristics { get; private set; }
    }
}