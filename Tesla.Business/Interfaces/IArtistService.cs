using Tesla.Data.Models;

namespace Tesla.Business.Interfaces;

public interface IArtistService
{
    public Task<Artist> FindById(int id);
    public Task<Artist> AddArtist(Artist artist);
    public Task<BaseMessage<Artist>> GetArtists();
    public Task<BaseMessage<Artist>> UpdateArtist(int id, Artist artist);
    Task<BaseMessage<Artist>> DeleteArtist(int id);
    Task GetAllArtist();
    Task FindArtistByName(string name);
    Task<BaseMessage<Artist>> GetAllArtists();
    Task<BaseMessage<Artist>> FindArtistsByName(string name);
}