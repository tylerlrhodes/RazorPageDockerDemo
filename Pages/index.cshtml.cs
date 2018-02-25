
using MongoDB.Driver;
using MongoDB.Bson;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace c_mongodb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MongoClient client;
        public IList<string> Names {get; private set; }

        public IndexModel()
        {
            client = new MongoClient("mongodb://mongodb:27017");

            var database = client.GetDatabase("foo");

            var collection = database.GetCollection<BsonDocument>("bar");

            var document = new BsonDocument
            {
                { "name", "Tyler" }
            };

            collection.InsertOne(document);
        }

        public async Task OnGetAsync()
        {
            var documents = await client.GetDatabase("foo")
                .GetCollection<BsonDocument>("bar")
                .Find(new BsonDocument())
                .ToListAsync();
            
            Names = new List<string>();

            if (documents != null)
            {
                foreach(var name in documents)
                {
                    Names.Add(name["name"].AsString);
                }
            }
        }
    }
}