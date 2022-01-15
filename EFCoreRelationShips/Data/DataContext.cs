using EFCoreRelationShips.entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationShips.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skills> Skills { get; set; }
    }
}
