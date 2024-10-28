using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ToDo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}
