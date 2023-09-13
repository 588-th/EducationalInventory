using System;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Consumable
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public string Photo { get; private set; }
        public int Count { get; private set; }
        public User ResponsibleUser { get; private set; }
        public User TemporarilyResponsibleUser { get; private set; }
        public string TypeConsumable { get; private set; }
        public ConsumableType ConsumableType { get; private set; }
    }
}