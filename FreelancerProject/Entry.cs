namespace FreelancerProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entry")]
    public partial class Entry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entry()
        {
            Contests = new HashSet<Contest>();
        }

        [Key]
        public int Entry_id { get; set; }

        [StringLength(5000)]
        public string Entry_header { get; set; }

        [StringLength(5000)]
        public string Entry_desc { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Entry_date { get; set; }

        public int? Rating { get; set; }

        [StringLength(50)]
        public string Entry_status { get; set; }

        public int? Contest_id { get; set; }

        public int? Freelancer_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contest> Contests { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual Freelancer Freelancer { get; set; }

        [StringLength(100)]
        public string ImageFile { get; set; }
    }
}
