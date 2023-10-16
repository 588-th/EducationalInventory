using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("Routes")]
    public class Route
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}