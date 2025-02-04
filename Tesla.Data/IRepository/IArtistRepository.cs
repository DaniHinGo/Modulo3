using Tesla.Data.Models;

namespace Tesla.Data.IRepository;

public interface IArtistRepository<TId, TEntity>
where TId: struct
where TEntity : BaseEntity<TId>
{
    Task AddAsync(TEntity artista);
    Task<TEntity> FindAsync(TId id);
}