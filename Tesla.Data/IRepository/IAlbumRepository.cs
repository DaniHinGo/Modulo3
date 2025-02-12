using Tesla.Data.Models;

namespace Tesla.Data.IRepository;

public interface IAlbumRepository<TId, TEntity>
where TId: struct
where TEntity : BaseEntity<TId>
{
    Task AddAsync(TEntity album);
    Task<TEntity> FindAsync(TId id);
    Task GetAllAlbums();
    Task<IEnumerable<Album>> GetAllAsync();
    Task<List<Album>> GetAllAlbum();
}