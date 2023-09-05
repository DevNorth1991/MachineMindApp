using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineMindApi.Models
{
    public class Recipe
    {
        [Key]//si dejaramos solo esta prpiedad tambien funcionaria 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//modo de controlar el auto incremento del id
        public int RecipeId { get; set; } // Primary Key
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<Angle> Angles { get; set; }
        public string MachineId { get; set; } // Foreign Key to link with Machine entity
        public Machine Machine { get; set; }
    }
}
