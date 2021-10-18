using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastucture.Data.Mongo.Options
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public int TimeoutInSeconds { get; set; }
    }
}
