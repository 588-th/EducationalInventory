using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ModelType
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string Code { get; private set; }
    }
}