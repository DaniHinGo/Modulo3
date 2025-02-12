using System.Net;
using Microsoft.EntityFrameworkCore;
using Tesla.Data.IRepository;
using Tesla.Data.Models;

namespace Tesla.Data.Repository;

public class ArtistRepository<TId, TEntity> : IArtistRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    internal NikolaContext _context;
    internal DbSet<TEntity> _dbSet;

    public ArtistRepository(NikolaContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    
    
    public virtual async Task AddAsync(TEntity artista)
    {
        
        await _dbSet.AddAsync(artista);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> FindAsync(TId id)
    {
        //var value = await _dbSet.FindAsync(id);
        try
        {
            //var value = await _context.Artists.FindAsync(id);    
            var response = await _dbSet.FindAsync(id);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            
        }
        return null;
        
    }

    public async Task<List<Artist>> GetAllArtist()
    {
        return await _context.Artists.ToListAsync();
    }

    public async Task<Artist> FindArtistById(TId id)
    {
        return await _context.Artists.FindAsync(id);

    }

    public async Task<List<Artist>> FindArtistByName(string name)
    {
        return await _context.Artists.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();
    }
    public async Task<Artist> UpdateArtist(Artist artist)
    {
        await _context.SaveChangesAsync();
        return artist;
    }

    public async Task DeleteArtist(TEntity artist)
    {
        _dbSet.Remove(artist);
        await _context.SaveChangesAsync();
    }
}