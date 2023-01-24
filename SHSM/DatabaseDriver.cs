using Microsoft.EntityFrameworkCore;
using SHSM.Devices;

namespace SHSM
{
    public class DatabaseDriver : DbContext
    {
        public DbSet<Light> Lights { get; set; }
        public DbSet<Door> Doors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=smarthome;uid=root;pwd=\"\";");
            System.Diagnostics.Trace.WriteLine("Connected to database: smarthome");
        }
    }

}
