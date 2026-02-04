using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await _context.TodoItems
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task AddAsync(TodoItem item)
    {
        item.UpdatedUtc = DateTime.UtcNow;
        await _context.TodoItems.AddAsync(item);
    }

    public async Task MarkDoneAsync(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item != null)
        {
            item.IsDone = true;
            item.UpdatedUtc = DateTime.UtcNow;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
