using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Route
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}