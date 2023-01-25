using Microsoft.EntityFrameworkCore;
using SHSM.Devices;

namespace SHSM
{
    public class DatabaseDriver : DbContext
    {
        public DbSet<Light> Lights { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=smarthome;uid=sh;pwd=\"pass\";");
            System.Diagnostics.Trace.WriteLine("Connected to database: smarthome");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                    .ToTable("Devices")
                    .HasDiscriminator<Type>("Type")
                    .HasValue<Light>(Type.LIGHT)
                    .HasValue<Door>(Type.DOOR);
        }
    }

}
