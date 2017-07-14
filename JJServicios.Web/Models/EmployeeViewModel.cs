using System;
using System.ComponentModel.DataAnnotations;

namespace JJServicios.Web.Models
{
    public class EmployeeViewModel
    {
        [ScaffoldColumn(false)]
        public long Id { get; set; }

        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "Requerido")]
        public string Identification { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Requerido")]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public string LastName { get; set; }

        [Display(Name = "Tipo cuenta")]
        [UIHint("FinancialAccountDataType")]
        public string FinancialAccount { get; set; }
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "FinancialAccount")]
        public long FinancialAccountId { get; set; }

        [Display(Name = "No cuenta")]
        [Required(ErrorMessage = "Requerido")]
        public string FinancialNumber { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "Requerido")]
        public string Mobile { get; set; }

        [Display(Name = "Otro teléfono")]
        public string OtherPhone { get; set; }
        public string Skype { get; set; }

        public string WhatsApp { get; set; }

        [Display(Name = "Email corporativo")]
        [Required(ErrorMessage = "Requerido")]
        public string CorporateEmail { get; set; }

        [Display(Name = "Otro correo")]
        public string OtherEmail { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "Requerido")]
        public string ResidenceCity { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }
        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        [Display(Name = "Fecha nacimiento")]
        public DateTime? Birthdate { get; set; }


        [Display(Name = "Cargo")]
        [UIHint("EmployeePositionDataType")]
        public string EmployeePosition { get; set; }
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "EmployeePosition")]
        public long EmployeePositionId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime UpdateDate { get; set; }
    }
}