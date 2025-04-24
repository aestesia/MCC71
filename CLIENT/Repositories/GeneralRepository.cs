using CLIENT.Base;
using CLIENT.Repositories.Interface;
using System.Net;
using System.Net.Http.Headers;

namespace CLIENT.Repositories
{
    public class GeneralRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        public readonly Address address;
        public readonly string request;
        public readonly IHttpContextAccessor contextAccessor;
        public readonly HttpClient httpClient;

        public GeneralRepository(Address address, string request)
        {
            this.address = address;
            this.request = request;
            contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public HttpStatusCode Delete(TId id)
        {
            var result = httpClient.DeleteAsync(request + id).Result;
            return result.StatusCode;
        }

        public Task<object> Get()
        {
            throw new NotImplementedException();
        }

        public Task<object> Get(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<object> Post(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Put(TEntity entity, TId id)
        {
            throw new NotImplementedException();
        }
    }
}
