using Inventory.Product.API.Entities.Abstraction;
using Inventory.Product.API.Extensions;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Reflection;

namespace Inventory.Product.API.Repositories.Abstraction;

public class MongoRepositoryBase<T> : IMongoRepositoryBase<T> where T : MongoEntity
{
    private IMongoDatabase MongoDatabase { get; }

    protected virtual IMongoCollection<T> Collection => MongoDatabase.GetCollection<T>(GetCollectionName());

    public MongoRepositoryBase(IMongoClient client, DatabaseSettings settings)
    {
        MongoDatabase = client.GetDatabase(settings.DatabaseName);
    }

    public async Task CreateAsync(T entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(string id)
    {
        await Collection.DeleteOneAsync(x => x.Id.Equals(id));
    }

    public IMongoCollection<T> FindAll(ReadPreference? readPreferance = null)
    {
        return MongoDatabase.WithReadPreference(readPreferance ?? ReadPreference.Primary)
            .GetCollection<T>(GetCollectionName());
    }

    public async Task UpdateAsync(T entity)
    {
        Expression<Func<T, string>> func = f => f.Id;
        var propertyName = func.Body.ToString().Split(".")[1]; // Extract property name
        var value = (string)entity.GetType()
            .GetProperty(propertyName)?.GetValue(entity, null);

        var filter = Builders<T>.Filter.Eq(func!, value);
        await Collection.ReplaceOneAsync(filter, entity);
    }

    private static string GetCollectionName()
    {
        return (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true)
            .FirstOrDefault() as BsonCollectionAttribute)?.CollectionName;
    }
}
