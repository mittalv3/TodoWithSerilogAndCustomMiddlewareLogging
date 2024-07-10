using Microsoft.EntityFrameworkCore;
using Todo.API.DbContexts;
using Todo.API.Entities;

namespace Todo.API.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;
        public ToDoRepository(ToDoDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            
        }

        public async Task<ToDo?> GetSingleToDoAsync(int Id)
        {
            return await _context.ToDos
                .Where(t => t.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ToDo>> GetToDosAsync()
        {
            return await _context.ToDos.OrderBy(t => t.Id).ToListAsync();
        }

        public void CreateToDo(ToDo toDo)
        {
            _context.Add(toDo);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteToDo(ToDo toDo)
        {
            _context.ToDos.Remove(toDo);
        }
    }
}
