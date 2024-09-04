using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoList.Stock.Db.Context;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Db.Repositories;

public interface IToDoListRepository
{
    Task<ToDoItem> AddToDo(ToDoItem todoItem);
    Task<IEnumerable<ToDoItem>> GetToDoByParams(Expression<Func<ToDoItem, bool>> predicate, params Expression<Func<ToDoItem, object>>[] includeProperties);
    Task<ToDoItem> GetToDoById(int id);
    Task<ToDoItem> UpdateToDo(ToDoItem todoItem);
    Task DeleteToDo(int todoId);
}

internal class ToDoListRepository : IToDoListRepository
{
    private StockContext _context;

    public ToDoListRepository(StockContext context)
    {
        _context = context;
    }

    public async Task<ToDoItem> AddToDo(ToDoItem todoItem)
    {
        var template = await _context.Set<ToDoItem>().AddAsync(todoItem);
        return template.Entity;
    }

    public async Task<ToDoItem> GetToDoById(int todoId)
    {
        var res = _context.Set<ToDoItem>().FirstOrDefaultAsync(p => p.Id == todoId && p.IsDeleted != true);
        return await res;
    }

    public async Task<IEnumerable<ToDoItem>> GetToDoByParams(Expression<Func<ToDoItem, bool>> predicate, params Expression<Func<ToDoItem, object>>[] includeProperties)
    {
        IQueryable<ToDoItem> query = _context.Set<ToDoItem>();
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.Where(predicate).ToListAsync();
    }

    public Task<ToDoItem> UpdateToDo(ToDoItem todoItem)
    {
        var template = _context.Entry(todoItem);
        template.State = EntityState.Modified;
        return Task.FromResult(template.Entity);
    }

    public async Task DeleteToDo(int todoId)
    {
        var comment = await _context.Set<ToDoItem>().FirstOrDefaultAsync(x => x.Id == todoId);
    }
}
