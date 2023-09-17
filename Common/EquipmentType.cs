using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class EquipmentType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}