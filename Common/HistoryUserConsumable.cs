using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryUserConsumable
    {
        [Key]
        public int Id { get; private set; }
        public User User { get; private set; }
        public Consumable Consumable { get; private set; }
        public DateTime Date { get; private set; }
    }
}