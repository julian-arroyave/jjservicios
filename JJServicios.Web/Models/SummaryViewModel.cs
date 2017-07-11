using System;

namespace JJServicios.Web.Models
{
    public class SummaryViewModel
    {
        public long MovementId { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; }
        public decimal AmountOut { get; set; }
        public decimal AmountIn { get; set; }
        public string Observations { get; set; }
        public Decimal Total { get; set; }
       
    }
}