using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class EquipmentType
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
    }
}