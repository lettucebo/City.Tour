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
    
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            this.Puzzles = new HashSet<Puzzle>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }
        public string Banner { get; set; }
        public string BannerThumb { get; set; }
        public bool IsDelete { get; set; }
        public System.DateTime ModifyTime { get; set; }
        public string Info { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Puzzle> Puzzles { get; set; }
    }
}