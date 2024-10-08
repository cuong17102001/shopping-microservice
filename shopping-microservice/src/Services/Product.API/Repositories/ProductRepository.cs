﻿using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<CatalogProduct, long, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
        { 
            
        }

        public Task CreateProduct(CatalogProduct product) => CreateAsync(product);

        public async Task DeleteProduct(long id)
        {
            var product = await GetProduct(id);
            if (product != null)
            {
                await DeleteAsync(product);
            }
        }

        public async Task<IEnumerable<CatalogProduct>> GetAllProducts()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<CatalogProduct> GetProduct(long id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<CatalogProduct> GetProductByNo(string productNo)
        {
            return await FindByCondition(x => x.No.Equals(productNo)).SingleOrDefaultAsync();
        }

        public Task UpdateProduct(CatalogProduct product) => UpdateAsync(product);
    }
}
