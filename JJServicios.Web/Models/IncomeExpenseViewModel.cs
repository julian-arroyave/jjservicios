using System;
using System.ComponentModel.DataAnnotations;

namespace JJServicios.Web.Models
{
    public class IncomeExpenseViewModel
    {
        public decimal Amount { get; set; }
        public string Observations { get; set; }
        [UIHint("MovementTypeDateType")]
        public string MovementType { get; set; }
        public long Id { get; set; }
        public long MovementTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}