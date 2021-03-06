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
    
    public partial class TourPuzzle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TourPuzzle()
        {
            this.TeamRecords = new HashSet<TeamRecord>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid PuzzleId { get; set; }
        public System.Guid TourId { get; set; }
        public int Sort { get; set; }
        public System.DateTime CreateTime { get; set; }
    
        public virtual Puzzle Puzzle { get; set; }
        public virtual Tour Tour { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamRecord> TeamRecords { get; set; }
    }
}
