using MachineMindApi.Models;
using MachineMindApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace MachineMindApi.Data
{
    public class ApplicationDBContext : DbContext
    {


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }


        public DbSet<Plant> Plants { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Angle> Angles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>().HasData(

                new Plant()
                {
                    PlantId = "EDI00226",
                    Name = "Planta de produccion 1 ",
                    ProductionLines = new List<ProductionLine>() // Lista vacía
                },
                 new Plant()
                 {
                     PlantId = "EDI00228",
                     Name = "Planta de produccion 2 ",
                     ProductionLines = new List<ProductionLine>() // Lista vacía
                 });

        }


    }
}
