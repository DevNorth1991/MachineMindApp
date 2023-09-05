using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineMindApi.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmployeeNumberId { get; set; } // Número de legajo como clave de acceso
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        // Otros atributos relevantes para la entidad Usuario

        // Relación de navegación inversa para las últimas modificaciones realizadas por el usuario
        public List<Angle> LastModifiedAngles { get; set; }
        public List<Parameter> LastModifiedParameters { get; set; }

    }
}


/*
popdriamos haber usado expresiones regulares para validar el mail tabien 
 [Required(ErrorMessage = "El campo Email es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El campo Email debe ser una dirección de correo electrónico válida.")]

 */