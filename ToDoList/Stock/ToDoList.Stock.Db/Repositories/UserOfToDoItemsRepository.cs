using Microsoft.EntityFrameworkCore;
using ToDoList.Stock.Db.Context;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Db.Repositories;

public interface IUserOfToDoItemsRepository
{
    Task<UsersOfToDoItems> AddConnection(UsersOfToDoItems items);
    Task<UsersOfToDoItems> UpdateConnection(UsersOfToDoItems items);
    //Task DeleteUser(int userId);
    //Task<User> GetUserByParams(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeProperties);
    //Task<IEnumerable<User>> GetAllUsers();
}

internal class UserOfToDoItemsRepository : IUserOfToDoItemsRepository
{
    private StockContext _context;

    public UserOfToDoItemsRepository(StockContext context)
    {
        _context = context;
    }

    public async Task<UsersOfToDoItems> AddConnection(UsersOfToDoItems items)
    {
        var template = await _context.Set<UsersOfToDoItems>().AddAsync(items);
        return template.Entity;
    }

    //public async Task<IEnumerable<User>> GetAllUsers()
    //{
    //    IQueryable<User> query = _context.Set<User>().Where(x => x.IsDeleted != true);
    //    return await query.ToListAsync();
    //}

    //public async Task<User> GetUserByParams(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeProperties)
    //{
    //    IQueryable<User> query = _context.Set<User>();
    //    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    //    return await query.SingleOrDefaultAsync(predicate);
    //}

    public Task<UsersOfToDoItems> UpdateConnection(UsersOfToDoItems items)
    {
        var template = _context.Entry(items);
        template.State = EntityState.Modified;
        return Task.FromResult(template.Entity);
    }

    //public async Task DeleteUser(int userId)
    //{
    //    var comment = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);
    //}
}

