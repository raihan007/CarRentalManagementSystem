using System.Data.Entity;
using CRMS_Data.Entities;

namespace CRMS_Data
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext(): base("DbContext") 
        {
            Database.SetInitializer<DbContext>(new CreateDatabaseIfNotExists<DbContext>());
        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //// Configure the primary key for the Region
            //modelBuilder.Entity<Login>()
            //    .HasKey(t => t.UserId);

            modelBuilder.Entity<Login>()
                .HasRequired(t => t.Customer)
                .WithRequiredPrincipal(t => t.Login).WillCascadeOnDelete(false);

            modelBuilder.Entity<Login>()
                 .HasRequired(t => t.Employee)
                 .WithRequiredPrincipal(t => t.Login).WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.ReservedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.BillCreated)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Reservation>()
            //    .Property(e => e.PickUpTime)
            //    .HasPrecision(4);

            //modelBuilder.Entity<Reservation>()
            //    .Property(e => e.DropOffTime)
            //    .HasPrecision(4);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Reservation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Vehicle)
                .HasForeignKey(e => e.ReservationVehicleId)
                .WillCascadeOnDelete(false);
        }
    }
}
