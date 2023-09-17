using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
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