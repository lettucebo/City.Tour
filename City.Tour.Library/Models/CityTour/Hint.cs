//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace City.Tour.Library.Models.CityTour
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hint
    {
        public System.Guid Id { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.Guid PuzzleId { get; set; }
        public bool IsDelete { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public string Content { get; set; }
        public int Sort { get; set; }
    
        public virtual Puzzle Puzzle { get; set; }
    }
}
