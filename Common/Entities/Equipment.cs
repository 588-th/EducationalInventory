using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    [Table("Equipments")]
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        [Required]
        public int Number { get; set; }
        public int EquipmentTypeId { get; set; }
        public int ModelTypeId { get; set; }
        public int AudienceId { get; set; }
        public int ResponsibleUserId { get; set; }
        public int TemporarilyResponsibleUserId { get; set; }
        public decimal Cost { get; set; }
        public int RouteId { get; set; }
        public int StatusId { get; set; }
        public int NetworkSettingsId { get; set; }
        public int ProgrammId { get; set; }
        public int ConsumableId { get; set; }
        public string Comment { get; set; }
    }
}