using System;
using System.ComponentModel.DataAnnotations;

namespace JJServicios.Web.Models
{
    public class EmployeeViewModel
    {
        [ScaffoldColumn(false)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public string LastName { get; set; }


        [UIHint("FinancialAccountDataType")]
        public string FinancialAccount { get; set; }
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "FinancialAccount")]
        public long FinancialAccountId { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string FinancialNumber { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string Mobile { get; set; }

        public string OtherPhone { get; set; }
        public string Skype { get; set; }

        public string WhatsApp { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string CorporateEmail { get; set; }

        public string OtherEmail { get; set; }

        [Required(ErrorMessage = "Requerido")]
        public string ResidenceCity { get; set; }

        public string Address { get; set; }
        [ScaffoldColumn(false)]
        public bool Active { get; set; }

        public DateTime? Birthdate { get; set; }


        //[UIHint("EmployeePositionDataType")]
        //public string EmployeePosition { get; set; }
        //[Required(ErrorMessage = "EmployeePosition")]
        //public long EmployeePositionId { get; set; }

        //[UIHint("FinancialAccountDataType")]
        //public string FinancialAccount { get; set; }
        //[Required(ErrorMessage = "FinancialAccount")]
        //public long FinancialAccountId { get; set; }

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