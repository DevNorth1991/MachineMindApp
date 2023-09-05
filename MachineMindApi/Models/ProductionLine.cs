using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineMindApi.Models
{
    public class ProductionLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductionLineId { get; set; } // Primary Key
        public string Name { get; set; }
        public List<Machine> Machines { get; set; }
        public string PlantId { get; set; } // Foreign Key to link with Plant entity
        public Plant Plant { get; set; }

    }
}
