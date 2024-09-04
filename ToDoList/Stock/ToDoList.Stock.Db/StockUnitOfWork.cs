using ToDoList.Db.Abstractions;
using ToDoList.Stock.Db.Context;
using ToDoList.Stock.Db.Repositories;

namespace ToDoList.Stock.Db;

public interface IStockUnitOfWork : IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IToDoListRepository ToDoListRepository { get; }
    IUserOfToDoItemsRepository UserOfToDoItemsRepository { get; }
}

public class StockUnitOfWork : IStockUnitOfWork
{
    private readonly StockContext _context;

    public StockUnitOfWork(StockContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    private IUserRepository _userRepository;

    public IUserRepository UserRepository
    {
        get
        {
            if (_userRepository != null) return _userRepository;
            return _userRepository = new UserRepository(_context);
        }
    }

    private IToDoListRepository _toDoListRepository;

    public IToDoListRepository ToDoListRepository
    {
        get
        {
            if (_toDoListRepository != null) return _toDoListRepository;
            return _toDoListRepository = new ToDoListRepository(_context);
        }
    }

    private IUserOfToDoItemsRepository _userOfToDoItemsRepository;

    public IUserOfToDoItemsRepository UserOfToDoItemsRepository
    {
        get
        {
            if (_userOfToDoItemsRepository != null) return _userOfToDoItemsRepository;
            return _userOfToDoItemsRepository = new UserOfToDoItemsRepository(_context);
        }
    }  

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveEntitiesAsync(cancellationToken);
    }
}