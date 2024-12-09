using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceDataAccess.Entities;

 public class UsersRepository : IRepository<User>
{
    private readonly IDbContextFactory<DeliveryServiceDbContext> _contextFactory;

    public UsersRepository(IDbContextFactory<DeliveryServiceDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IEnumerable<User> GetAll()
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<User>().AsNoTracking().Include(e => e.Permissions).ToList();
    }

    public IEnumerable<User> GetAll(Expression<Func<User, bool>> predicate)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<User>().AsNoTracking().Include(e => e.Permissions).Where(predicate).ToList();
    }

    public User? GetById(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<User>().AsNoTracking().Include(e => e.Permissions).FirstOrDefault(e => e.Id == id);
    }

    public User GetById(Guid id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<User>().AsNoTracking().Include(e => e.Permissions).FirstOrDefault(e => e.ExternalId == id);
    }

    public User Save(User entity)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        if (dbContext.Set<User>().AsNoTracking().FirstOrDefault(e => e.Id == entity.Id) == null)
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = DateTime.UtcNow;
            var result = dbContext.Set<User>().Add(entity);
            dbContext.SaveChanges();
            return result.Entity;
        }
        else
        {
            entity.ModificationTime = DateTime.UtcNow;
            var result = dbContext.Set<User>().Update(entity);
            dbContext.SaveChanges();
            return result.Entity;
        }
    }

    public void Delete(User entity)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Set<User>().Remove(entity);
        dbContext.SaveChanges();
    }
}