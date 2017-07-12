using System;

namespace JJServicios.Web.Models
{
    public class SummaryViewModel
    {
        public long Id { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; }
        public decimal AmountOut { get; set; }
        public decimal AmountIn { get; set; }
        public string AgentName  {get; set;}

        public string AmountOutString
        {
            get
            {
                if (AmountOut == 0) return string.Empty;
                
                return AmountOut.ToString("#,##0.00");

            }
        }

        public string AmountInString
        {
            get
            {
                if (AmountIn == 0) return string.Empty;

                return AmountIn.ToString("#,##0.00");

            }
        }

        public string Observations { get; set; }
        public Decimal Total { get; set; }

        public string TotalString => Total.ToString("#,##0.00");
    }
}