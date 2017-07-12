﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration.Conventions;

namespace JJServicios.DB.Contracts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JJServiciosEntities : DbContext
    {
        public JJServiciosEntities()
            : base("name=JJServiciosEntities")
        {
            Database.SetInitializer<JJServiciosEntities>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePosition { get; set; }
        public virtual DbSet<Expense> Expense { get; set; }
        public virtual DbSet<FinancialAccount> FinancialAccount { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<MovementType> MovementType { get; set; }
        public virtual DbSet<PaymentEmployee> PaymentEmployee { get; set; }
        public virtual DbSet<ServiceMovement> ServiceMovement { get; set; }
        public virtual DbSet<WorkTimeLog> WorkTimeLog { get; set; }
    }
}