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
    
    public partial class Master_Menus
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string IsActive { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> Order_no { get; set; }
    }
}
