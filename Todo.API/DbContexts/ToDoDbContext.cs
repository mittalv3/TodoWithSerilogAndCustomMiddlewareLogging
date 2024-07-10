using Microsoft.EntityFrameworkCore;
using Todo.API.Entities;
using Todo.API.Models;

namespace Todo.API.DbContexts
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
        }
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .HasData(
                new ToDo("Install Visual Studio")
                {
                    Id = 1,
                    IsCompleted = true,
                    DueDate = new DateOnly(2024, 04, 05),
                    Priority = "Red"
                }, 
                new ToDo("Configure New Project")
                {
                    Id = 2,
                    IsCompleted = false,
                    DueDate = new DateOnly(2024, 04, 06),
                    Priority = "Red"
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
