using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class NetworkSetting
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Ip { get; private set; }
        public string Mask { get; private set; }
        public string Gateway { get; private set; }
        public List<string> Dns { get; private set; }
    }
}