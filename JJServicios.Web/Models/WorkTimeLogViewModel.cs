using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JJServicios.Web.Models
{
    public class WorkTimeLogViewModel
    {
        [ScaffoldColumn(false)]
        public long Id { get; set; }
        [ScaffoldColumn(false)]
        public long AgentId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string Observations { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime UpdateDate { get; set; }
        [ScaffoldColumn(false)]
        public string Agent  { get; set; }
        public string WorkedHours{get; set;}
        
    }
}