//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace chat_rooms.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class message
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int ID_Sender { get; set; }
        public int ID____Receiver { get; set; }
        public bool seen { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}