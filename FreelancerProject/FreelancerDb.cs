namespace FreelancerProject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FreelancerDb : DbContext
    {
        public FreelancerDb()
            : base("name=FreelancerDb1")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Contest> Contests { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Freelancer> Freelancers { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Client_name)
                .IsUnicode(false);

            modelBuilder.Entity<Contest>()
                .Property(e => e.Contest_name)
                .IsUnicode(false);

            modelBuilder.Entity<Contest>()
                .Property(e => e.Contest_desc)
                .IsUnicode(false);

            modelBuilder.Entity<Contest>()
                .Property(e => e.Contest_category)
                .IsUnicode(false);

            modelBuilder.Entity<Contest>()
                .Property(e => e.Contest_status)
                .IsUnicode(false);

            modelBuilder.Entity<Contest>()
                .HasMany(e => e.Entries)
                .WithOptional(e => e.Contest)
                .HasForeignKey(e => e.Contest_id);

            modelBuilder.Entity<Entry>()
                .Property(e => e.Entry_header)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.Entry_desc)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.Entry_status)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .HasMany(e => e.Contests)
                .WithOptional(e => e.Entry)
                .HasForeignKey(e => e.Winner_id);

            modelBuilder.Entity<Freelancer>()
                .Property(e => e.Freelancer_name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email_id)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
