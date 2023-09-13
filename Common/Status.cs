using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Status
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
    }
}