using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineMindApi.Models
{
    public class Parameter
    {
        [Key]//si dejaramos solo esta prpiedad tambien funcionaria 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//modo de controlar el auto incremento del id
        public int ParameterId { get; set; } // Primary Key
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Value { get; set; }
        public int RecipeId { get; set; } // Foreign Key to link with Recipe entity
        public Recipe Recipe { get; set; }

        public DateTime CreatedDate { get; set; } // Fecha de creación (opcional, si lo necesitas)
        public DateTime LastModifiedDate { get; set; } // Fecha de última modificación
        public string LastModifiedByUserId { get; set; } // Id del usuario que realizó la última modificación (FK a la entidad Usuario)
        public User LastModifiedByUser { get; set; } // Usuario que realizó la última modificación (relación de navegación)
    }
}
