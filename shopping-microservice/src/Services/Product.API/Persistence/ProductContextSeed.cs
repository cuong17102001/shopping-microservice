
using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
        {
            if (productContext == null)
            {
                productContext.AddRangeAsync(GetCatalogProducts());
                await productContext.SaveChangesAsync();
                logger.Information("Seeded data for Product Db");
            }
        }

        private static IEnumerable<CatalogProduct> GetCatalogProducts()
        {
            return new List<CatalogProduct>
            {
                new CatalogProduct
                {
                    No = "Lotus",
                    Name = "Esprit",
                    Summary = "test summary 1",
                    Description = "Description 1",
                    Price = (decimal)1777.46
                },
                new CatalogProduct
                {
                    No = "hehe",
                    Name = "Davis",
                    Summary = "test summary 2",
                    Description = "Description 2",
                    Price = (decimal)12345.46
                }
            };
        }
    }
}
