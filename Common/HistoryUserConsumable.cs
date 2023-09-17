using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryUserConsumable
    {
        [Key]
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int ConsumableId { get; private set; }
        public string Date { get; private set; }
    }
}