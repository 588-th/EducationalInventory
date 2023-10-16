using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("Programms")]
    public class Programm
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DeveloperId { get; set; }
        public string Version { get; set; }
    }
}