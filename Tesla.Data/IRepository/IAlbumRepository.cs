using Tesla.Data.Models;

namespace Tesla.Data.Repositoty;

public interface IAlbumRepository<TId, TEntity>
where TId: struct
where TEntity : BaseEntity<TId>
{
    Task AddAsync(TEntity album);
    Task<TEntity> FindAsync(TId id);
    Task<IEnumerable<Album>> GetAllAsync();
}