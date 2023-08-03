namespace RepositoryInterfaces.GenericInterfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleAsync(Guid id);
        Task<TEntity> InsertAsync(TEntity entity);

    }

}
