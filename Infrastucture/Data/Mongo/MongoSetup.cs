using FravegaService.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastucture.Data.Mongo
{
    public static class MongoSetup
    {
        public static void OnStartup()
        {
            MongoDefaults.GuidRepresentation = GuidRepresentation.Standard;

            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(new NullableSerializer<decimal>(
                new DecimalSerializer(BsonType.Decimal128)));

            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

          
            BsonClassMap.RegisterClassMap<Promotion>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });
        }
    }
}
