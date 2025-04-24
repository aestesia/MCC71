using System.Net;

namespace CLIENT.Repositories.Interface
{
    public interface IRepository<T, X> where T : class
    {
        Task<Object> Get();
        Task<Object> Get(X id);
        Task<Object> Post(T entity);
        HttpStatusCode Put(T entity, X id);
        HttpStatusCode Delete(X id);
    }
}
