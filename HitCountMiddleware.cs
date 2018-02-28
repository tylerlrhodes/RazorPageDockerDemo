

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace c_mongodb
{
    public class HitCountMiddleware
    {
        private readonly RequestDelegate _next;
        private object _lockRedis = new object();

        public HitCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ConnectionMultiplexer multiplexer)
        {
            IDatabase db = multiplexer.GetDatabase();

            lock(_lockRedis)
            {
                int count = (int)db.Wait(db.StringGetAsync("hits"));

                count++;

                db.StringSet("hits", count);
            }
            
            await _next(httpContext);
        }
    }
}
