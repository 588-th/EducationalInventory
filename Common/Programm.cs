using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Programm
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public int DeveloperId { get; private set; }
        public string Version { get; private set; }
    }
}