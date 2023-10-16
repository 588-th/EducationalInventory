using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("Audiences")]
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