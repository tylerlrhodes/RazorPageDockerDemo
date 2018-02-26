

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace c_mongodb
{
    public class HitCountMiddleware
    {
        private readonly RequestDelegate _next;

        public HitCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ConnectionMultiplexer multiplexer)
        {
            IDatabase db = multiplexer.GetDatabase();

            int count = (int)db.StringGet("hits");

            count++;

            db.StringSet("hits", count);
            
            await _next(httpContext);
        }
    }
}
