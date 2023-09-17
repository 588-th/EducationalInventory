﻿using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class ConsumableCharacteristics
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int ConsumableCharacteristicsValuesId { get; private set; }
    }
}