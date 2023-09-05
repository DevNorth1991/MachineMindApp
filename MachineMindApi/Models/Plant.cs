using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineMindApi.Models
{
    public class Plant
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(50)]
        public string PlantId { get; set; } // Primary Key
        [ Required,MaxLength(50)]
        public string Name { get; set; }

        public List<ProductionLine> ProductionLines { get; set; } 
    }
}
