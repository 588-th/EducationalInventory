using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ModelType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
    }
}