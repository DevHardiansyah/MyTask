﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyTask.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBMytaskEntities : DbContext
    {
        public DBMytaskEntities()
            : base("name=DBMytaskEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Master_Menus> Master_Menus { get; set; }
        public virtual DbSet<Master_Roles> Master_Roles { get; set; }
        public virtual DbSet<Master_Users> Master_Users { get; set; }
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<ViewGroupUser> ViewGroupUsers { get; set; }
        public virtual DbSet<ViewGroupMenu> ViewGroupMenus { get; set; }

        public virtual int spSendEmail()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spSendEmail");
        }
    }
}