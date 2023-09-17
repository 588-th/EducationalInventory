using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Audience
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int ResponsibleUserId { get; set; }
        public int TemporarilyResponsibleUserId { get; set; }
    }
}