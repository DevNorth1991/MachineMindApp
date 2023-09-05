using System.ComponentModel.DataAnnotations;

namespace MachineMindApi.Models.Dto
{
    public class PlantDto
    {
        [Required, MaxLength(50)]
        public string PlantId { get; set; } // Primary Key
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
