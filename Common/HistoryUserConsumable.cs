using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryUserConsumable
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ConsumableId { get; set; }
        public string Date { get; set; }
    }
}