using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}