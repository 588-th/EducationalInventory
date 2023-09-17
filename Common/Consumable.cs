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
        public string ReceiptDate { get; private set; }
        public string Photo { get; private set; }
        public int Count { get; private set; }
        public int ResponsibleUserId { get; private set; }
        public int TemporarilyResponsibleUserId { get; private set; }
        public int ConsumableTypeId { get; private set; }
    }
}