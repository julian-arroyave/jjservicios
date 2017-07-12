using System.ComponentModel.DataAnnotations;


namespace JJServicios.Web.Models
{
    public class WorkTimeLogViewModel
    {
        public long Id { get; set; }
        public long AgentId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public string Observations { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string Agent  { get; set; }
        public string WorkedHours{get; set;}
        
    }
}