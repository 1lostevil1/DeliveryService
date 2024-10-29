namespace DeliveryServiceDataAccess.Entities;

public interface IRepository<T> where T: BaseEntity
{
    IQueryable<T> GetAllProducts();
    IQueryable<T> GetAllOrders();
    T? GetProductById(int id);
    T? GetOrderById(Guid id);
    T SaveProduct(T entity);
    T SaveOrder(T entity);
    T saveUser(T entity);
    void Delete(T entity);
}