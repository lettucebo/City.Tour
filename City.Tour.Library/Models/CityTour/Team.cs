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
    
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            this.TeamRecords = new HashSet<TeamRecord>();
            this.Users = new HashSet<User>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string InviteCode { get; set; }
        public System.Guid TourId { get; set; }
        public Nullable<System.Guid> CurrentPuzzleId { get; set; }
        public int CurrentPuzzleNum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamRecord> TeamRecords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Puzzle Puzzle { get; set; }
    }
}
