//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyTask.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuRole
    {
        public int Id { get; set; }
        public Nullable<int> Role_id { get; set; }
        public Nullable<int> Menu_id { get; set; }
        public string IsCreated { get; set; }
        public string IsRead { get; set; }
        public string IsUpdate { get; set; }
        public string IsDelete { get; set; }
    }
}