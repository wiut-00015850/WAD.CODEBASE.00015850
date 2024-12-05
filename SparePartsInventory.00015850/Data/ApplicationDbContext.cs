using Microsoft.EntityFrameworkCore;
using SparePartsInventory._00015850.Models.Entities;

namespace SparePartsInventory._00015850.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Producer> Producers { get; set; }
    }
}
