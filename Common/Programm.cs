using System.ComponentModel.DataAnnotations;

namespace Common
{
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