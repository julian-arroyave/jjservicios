//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JJServicios.DB.Contracts
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkTimeLog
    {
        public long Id { get; set; }
        public long AgentId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Observations { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    
        public virtual Agent Agent { get; set; }
    }
}
