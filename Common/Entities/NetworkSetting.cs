using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("NetworkSettings")]
    public class NetworkSetting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Ip { get; set; }
        public string Mask { get; set; }
        public string Gateway { get; set; }
        public string Dns { get; set; }
    }
}