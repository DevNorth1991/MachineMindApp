using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineMindApi.Models
{
    public class Machine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MachineId { get; set; } // Primary Key
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Recipe> Recipes { get; set; }
        public string ProductionLineId { get; set; } // Foreign Key to link with ProductionLine entity
        public ProductionLine ProductionLine { get; set; }
    }
}
