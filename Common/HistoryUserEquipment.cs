using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryUserEquipment
    {
        [Key]
        public int Id { get; private set; }
        public User User { get; private set; }
        public Equipment Equipment { get; private set; }
        public DateTime Date { get; private set; }
    }
}