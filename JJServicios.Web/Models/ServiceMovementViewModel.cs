using System;
using System.ComponentModel.DataAnnotations;

namespace JJServicios.Web.Models
{
    public class ServiceMovementViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string City { get; set; }

        [Range(typeof(decimal), "0.0001", "79228162514264337593543950335", ErrorMessage = "Debe de ingresar una cantidad válida")]
        [Required(ErrorMessage = "Requerido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Observations { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string STFRequirement { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Support { get; set; }

        [UIHint("MovementTypeDateType")]
        public string MovementType { get; set; }
        [Required(ErrorMessage = "Debe ingresar el tipo de movimiento")]
        public long MovementTypeId { get; set; }

        [UIHint("EmployeeDataType")]
        public string Employee { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public long EmployeeId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}