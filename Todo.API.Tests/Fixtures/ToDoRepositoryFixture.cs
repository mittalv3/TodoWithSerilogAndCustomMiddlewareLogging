using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.API.DbContexts;
using Todo.API.Entities;

namespace Todo.API.Tests.Fixtures
{
    public class ToDoRepositoryFixture : IDisposable
    {
        public readonly DbContextOptionsBuilder<ToDoDbContext>? _dbContextOptions;
        public readonly ToDoDbContext _context;


        public ToDoRepositoryFixture()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            _context = new ToDoDbContext(_dbContextOptions.Options);
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
