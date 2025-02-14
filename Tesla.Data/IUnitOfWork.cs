using Tesla.Data.Models;

namespace Tesla.Data;

public interface IUnitOfWork
{
    IRepository<int, Artist> ArtistRepository{get;}
    IRepository<int, Album> AlbumRepository{get;}
    Task SaveAsync();
}