using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Equipment
    {
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string Photo { get; private set; }
        [Required]
        public int Number { get; private set; }
        public int TypeEquipmentId { get; private set; }
        public int TypeModelId { get; private set; }
        public int AudienceId { get; private set; }
        public int ResponsibleUserId { get; private set; }
        public int TemporarilyResponsibleUserId { get; private set; }
        public decimal Cost { get; private set; }
        public int RouteId { get; private set; }
        public int StatusId { get; private set; }
        public int NetworkSettingsId { get; private set; }
        public int ProgrammId { get; private set; }
        public int ConsumableId { get; private set; }
        public string Comment { get; private set; }
    }
}