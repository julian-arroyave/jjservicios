using System;
using System.ComponentModel.DataAnnotations;

namespace JJServicios.Web.Models
{
    public class IncomeExpenseViewModel
    {
        [Range(typeof(decimal), "0.0001", "79228162514264337593543950335", ErrorMessage = "Debe de ingresar una cantidad válida")]
        [Required(ErrorMessage = "Debe ingresar una cantidad")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Debe ingresar alguna observación")]
        public string Observations { get; set; }
        [UIHint("MovementTypeDateType")]
        public string MovementType { get; set; }
        public long Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el tipo de movimiento")]
        public long MovementTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public long BankAccountId { get; set; }
    }
}