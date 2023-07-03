using Domains;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todo { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TodoDB;Username=postgres;Password= ");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasKey(t => t.Id);
            modelBuilder.Entity<Todo>()
                .HasMany(t => t.Subtasks).WithOne(s => s.Todo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("TodoId").IsRequired();

            base.OnModelCreating(modelBuilder);
        }

    }
}
