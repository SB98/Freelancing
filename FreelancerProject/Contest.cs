namespace FreelancerProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contest")]
    public partial class Contest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contest()
        {
            Entries = new HashSet<Entry>();
            
        }

        [Key]
        public int Contest_id { get; set; }

        [StringLength(200)]
        public string Contest_name { get; set; }

        [StringLength(5000)]
        public string Contest_desc { get; set; }

        [StringLength(5000)]
        public string Contest_category { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Contest_date { get; set; }

        public int? Duration { get; set; }

        public int? Prize { get; set; }

        [StringLength(50)]
        public string Contest_status { get; set; }

        public int? Client_id { get; set; }

        public int? Winner_id { get; set; }

        public virtual Client Client { get; set; }

        public virtual Entry Entry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entry> Entries { get; set; }

        
    }
}
