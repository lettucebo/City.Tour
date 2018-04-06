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
    
    public partial class Puzzle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Puzzle()
        {
            this.Answers = new HashSet<Answer>();
            this.Hints = new HashSet<Hint>();
            this.Tours = new HashSet<Tour>();
            this.Tours1 = new HashSet<Tour>();
            this.Tours2 = new HashSet<Tour>();
            this.Tours3 = new HashSet<Tour>();
            this.Tours4 = new HashSet<Tour>();
            this.Tours5 = new HashSet<Tour>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.Guid TourId { get; set; }
        public bool IsDelete { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }
        public string Picture { get; set; }
        public Nullable<int> Sort { get; set; }
        public System.DateTime ModifyTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hint> Hints { get; set; }
        public virtual Tour Tour { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours5 { get; set; }
    }
}
