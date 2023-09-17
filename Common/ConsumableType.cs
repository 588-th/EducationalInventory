using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ConsumableType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConsumableCharacteristicsId { get; set; }
    }
}