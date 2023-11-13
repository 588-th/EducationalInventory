﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("HistoryUserEquipments")]
    public class HistoryUserEquipment
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Equipment { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
    }
}