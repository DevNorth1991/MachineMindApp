using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineMindApi.Models
{
    public class Angle
    {
        [Key]//si dejaramos solo esta prpiedad tambien funcionaria 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//modo de controlar el auto incremento del id
        public int AngleId { get; set; } // Primary Key
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(0, 360, ErrorMessage = "El valor debe estar entre 0 y 360.")]
        public int InitialValue { get; set; }
        [Range(0, 360, ErrorMessage = "El valor debe estar entre 0 y 360.")]
        public int FinalValue { get; set; }
        public int RecipeId { get; set; } // Foreign Key to link with Recipe entity
        public Recipe Recipe { get; set; }

        public DateTime CreatedDate { get; set; } // Fecha de creación (opcional, si lo necesitas)
        public DateTime LastModifiedDate { get; set; } // Fecha de última modificación
        public string LastModifiedByUserId { get; set; } // Id del usuario que realizó la última modificación (FK a la entidad Usuario)
        public User LastModifiedByUser { get; set; } // Usuario que realizó la última modificación (relación de navegación)
    }
}
