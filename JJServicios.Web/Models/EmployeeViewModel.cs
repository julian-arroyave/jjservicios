using System;
using System.ComponentModel.DataAnnotations;

namespace JJServicios.Web.Models
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string City { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string FinancialNumber { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Mobile { get; set; }

        public string WhatsApp { get; set; }
        public string OtherNumber { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string CorporateEmail { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string OtherEmail { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string ResidenceCity { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }

        public bool Birthdate { get; set; }


        [UIHint("EmployeePositionDataType")]
        public string EmployeePosition { get; set; }
        [Required(ErrorMessage = "EmployeePosition")]
        public long EmployeePositionId { get; set; }

        [UIHint("FinancialAccountDataType")]
        public string FinancialAccount { get; set; }
        [Required(ErrorMessage = "FinancialAccount")]
        public long FinancialAccountId { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}