using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoList.Stock.Db.Context;
using ToDoList.Stock.Db.Domain;

namespace ToDoList.Stock.Db.Repositories;

public interface IUserRepository
{
    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(int userId);
    Task<User> GetUserByParams(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeProperties);
    Task<IEnumerable<User>> GetAllUsers();
}

internal class UserRepository : IUserRepository
{
    private StockContext _context;

    public UserRepository(StockContext context)
    {
        _context = context;
    }

    public async Task<User> AddUser(User user)
    {
        var template = await _context.Set<User>().AddAsync(user);
        return template.Entity;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        IQueryable<User> query = _context.Set<User>().Where(x => x.IsDeleted != true);
        return await query.ToListAsync();
    }

    public async Task<User> GetUserByParams(Expression<Func<User, bool>> predicate, params Expression<Func<User, object>>[] includeProperties)
    {
        IQueryable<User> query = _context.Set<User>();
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.SingleOrDefaultAsync(predicate);
    }

    public Task<User> UpdateUser(User user)
    {
        var template = _context.Entry(user);
        template.State = EntityState.Modified;
        return Task.FromResult(template.Entity);
    }

    public async Task DeleteUser(int userId)
    {
        var comment = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);
    }
}