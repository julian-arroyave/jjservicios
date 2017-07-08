//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JJServicios.DB.Interface
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.PaymentEmployee = new HashSet<PaymentEmployee>();
            this.ServiceMovement = new HashSet<ServiceMovement>();
        }
    
        public long Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public long FinancialNumber { get; set; }
        public string Mobile { get; set; }
        public string OtherPhone { get; set; }
        public string WhatsApp { get; set; }
        public string Skype { get; set; }
        public string CorporateEmail { get; set; }
        public string OtherEmail { get; set; }
        public string ResidenceCity { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public long EmployeePositionId { get; set; }
        public long FinancialAccountId { get; set; }
        public long AgentId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual EmployeePosition EmployeePosition { get; set; }
        public virtual FinancialAccount FinancialAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentEmployee> PaymentEmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceMovement> ServiceMovement { get; set; }
    }
}
