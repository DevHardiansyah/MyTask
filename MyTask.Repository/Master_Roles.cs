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
    
    public partial class Master_Roles
    {
        public int Id { get; set; }
        public string Role_Name { get; set; }
        public string Role_Desccription { get; set; }
        public string IsActive { get; set; }
        public string Created_by { get; set; }
        public Nullable<System.DateTime> Created_date { get; set; }
        public string Updated_by { get; set; }
        public Nullable<System.DateTime> Updated_date { get; set; }
    }
}
