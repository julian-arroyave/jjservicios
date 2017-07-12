using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JJServicios.Web.Models
{
    public class AgentViewModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }

        public long MovementId { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; }
        public decimal AmountOut { get; set; }
        public decimal AmountIn { get; set; }
        public string Observations { get; set; }
        public Decimal Total { get; set; }
    }
}