using Tesla.Business.Interfaces;
using Tesla.Data.IRepository;
using Tesla.Data.Models;
using Tesla.Data.Repository;

namespace Tesla.Business.Services;


public class ArtistService : IArtistService
{
    private readonly NikolaContext _context;
    private IArtistRepository<int, Artist> _artistRepository;

    public ArtistService(NikolaContext context)
    {
        _context = context;
        _artistRepository = new ArtistRepository<int, Artist>(_context);
    }
    
    public async Task<Artist> AddArtist(Artist artist)
    {
        await _artistRepository.AddAsync(artist);
        return artist;
    }

    public Task<BaseMessage<Artist>> DeleteArtist(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public Task FindArtistByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> FindArtistsByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Artist> FindById(int id)
    {
        var artist = await _artistRepository.FindAsync(id);
        return  artist;
    }

    public Task GetAllArtist()
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> GetAllArtists()
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> GetArtists()
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> UpdateArtist(int id, Artist artist)
    {
        throw new NotImplementedException();
    }

    Task<BaseMessage<Artist>> IArtistService.FindArtistsByName(string name)
    {
        throw new NotImplementedException();
    }
}
