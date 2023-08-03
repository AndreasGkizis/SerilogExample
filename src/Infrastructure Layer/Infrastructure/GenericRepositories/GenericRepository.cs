﻿using Database;
using Microsoft.EntityFrameworkCore;
using RepositoryInterfaces.GenericInterfaces;

namespace Infrastructure.GenericRepositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class
    {
        private readonly LoggingDbContext _loggingDbContext;

        public GenericRepository(LoggingDbContext loggingDbContext)
        {
            _loggingDbContext = loggingDbContext;
        }


        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await _loggingDbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity> GetSingleAsync(Guid id)
        {
            try
            {
                return await _loggingDbContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                var result = await _loggingDbContext.Set<TEntity>().AddAsync(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
