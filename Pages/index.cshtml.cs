
using MongoDB.Driver;
using MongoDB.Bson;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using StackExchange.Redis;

namespace c_mongodb.Pages
{
    public class Test
    {
        public ObjectId Id { get; set; }
        public DateTime time {get; set;}
        
    }

    public class IndexModel : PageModel
    {
        private readonly MongoClient client;
        private readonly ConnectionMultiplexer multiplexer;
        public IList<Test> Times {get; private set; }
        public int Hits {get; private set;}
        public IndexModel(ConnectionMultiplexer mult)
        {
            multiplexer = mult;

            client = new MongoClient("mongodb://mongodb:27017");

            var database = client.GetDatabase("foo");

            var collection = database.GetCollection<Test>("Test");

            collection.InsertMany( new List<Test> {
                new Test { time = DateTime.Now }
            });
        }

        public async Task OnGetAsync()
        {
            Times = await client.GetDatabase("foo")
                .GetCollection<Test>("Test")
                .Find(new BsonDocument())
                .ToListAsync();

            IDatabase db = multiplexer.GetDatabase();

            Hits = (int)db.Wait(db.StringGetAsync("hits"));            
        }
    }
}