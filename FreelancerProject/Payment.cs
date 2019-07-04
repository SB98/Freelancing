namespace FreelancerProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        public int Payment_id { get; set; }

        public int? Payment_amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Payment_date { get; set; }

        public int? Contest_id { get; set; }

        public int? Freelancer_id { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual Freelancer Freelancer { get; set; }
    }
}
