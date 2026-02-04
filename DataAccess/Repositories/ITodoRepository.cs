using DataAccess.Models;

namespace DataAccess.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<IEnumerable<TodoItem>> GetByIdsAsync(IEnumerable<int> ids);
    Task<TodoItem?> GetByIdAsync(int id);
    Task AddAsync(TodoItem item);
    Task MarkDoneAsync(int id);
    Task SaveChangesAsync();
}
