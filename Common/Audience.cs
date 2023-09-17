using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Audience
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public int ResponsibleUserId { get; private set; }
        public int TemporarilyResponsibleUserId { get; private set; }
    }
}