//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLTV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTPTT
    {
        public int MATK { get; set; }
        public string MAS { get; set; }
        public int SOLUONGN { get; set; }
        public int TONG { get; set; }
    
        public virtual SACH SACH { get; set; }
        public virtual PHIEUTRATIEN PHIEUTRATIEN { get; set; }
    }
}