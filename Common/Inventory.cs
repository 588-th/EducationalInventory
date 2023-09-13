using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Inventory
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDare { get; private set; }
        public DateTime EndDate { get; private set; }
        public Equipment Equipment { get; private set; }
        public User User { get; private set; }
    }
}