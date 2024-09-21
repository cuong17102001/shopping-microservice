﻿using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext> : RepositoryQueryBase<T, K, TContext>, IRepositoryBaseAsync<T, K, TContext>
        where T : EntityBase<K>
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        public RepositoryBaseAsync(TContext dbContext, IUnitOfWork<TContext> unitOfWork) : base(dbContext) 
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task<IDbContextTransaction> BeginTransactionAsync() => _context.Database.BeginTransactionAsync();

        public K Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity.Id;
        }

        public async Task<K> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveChangesAsync();
            return entities.Select(x => x.Id).ToList();
        }

        public async void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }

        public Task RollbackTransactionAsync() => _context.Database.RollbackTransactionAsync();

        public Task<int> SaveChangesAsync() => _unitOfWork.CommitAsync();

        public async Task UpdateAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Unchanged) return;

            T exist = _context.Set<T>().Find(entity.Id);
            _context.Entry(exist).CurrentValues.SetValues(entity);

            await SaveChangesAsync();
        }

        public async Task UpdateListAsync(IEnumerable<T> entities) 
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await SaveChangesAsync();
        }
    }
}
