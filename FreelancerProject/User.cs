namespace FreelancerProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        [StringLength(50)]
        public string Email_id { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? Freelancer_id { get; set; }

        public int? Client_id { get; set; }

        public virtual Client Client { get; set; }

        public virtual Freelancer Freelancer { get; set; }
    }
}
