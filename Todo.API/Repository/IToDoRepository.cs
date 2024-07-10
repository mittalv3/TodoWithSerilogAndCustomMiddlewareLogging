using Todo.API.Entities;
using Todo.API.Models;

namespace Todo.API.Repository
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDo>> GetToDosAsync();
        Task<ToDo?> GetSingleToDoAsync(int Id);
        void CreateToDo(ToDo toDo);
        Task<bool> SaveChangesAsync();

        void DeleteToDo(ToDo toDo);
    }
}
