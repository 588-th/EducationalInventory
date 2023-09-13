using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Auditoruim
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public User ResponsibleUser { get; private set; }
        public User TemporarilyResponsibleUser { get; private set; }
    }
}