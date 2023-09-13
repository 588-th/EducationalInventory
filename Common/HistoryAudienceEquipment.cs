using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class HistoryAudienceEquipment
    {
        [Key]
        public int Id { get; private set; }
        public Auditoruim Auditoruim { get; private set; }
        public Equipment Equipment { get; private set; }
        public DateTime Date { get; private set; }
    }
}