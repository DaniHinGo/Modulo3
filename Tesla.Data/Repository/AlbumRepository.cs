using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Tesla.Data.IRepository;
using Tesla.Data.Models;

namespace Tesla.Data.Repository;

public class AlbumRepository<TId, TEntity> : IAlbumRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    internal NikolaContext _context;
    internal DbSet<TEntity> _dbSet;

    public AlbumRepository(NikolaContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    
    
    public virtual async Task AddAsync(TEntity album)
    {
        
        await _dbSet.AddAsync(album);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> FindAsync(TId id)
    {
        //var value = await _dbset.FindAsync(id);
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

    public async Task<IEnumerable<Album>> GetAllAsync()
    {
        return await _context.Albums
            .Include(x => x.Artist)
            .ToListAsync();
    }

    public async Task<TEntity> AddAlbum(TEntity album)
    {
        await _dbSet.AddAsync(album);
        await _context.SaveChangesAsync();
        return album;
    }

    public async Task<Album> FindAlbumById(TId id)
    {
        return await _context.Albums.FindAsync(id);
    }

    public async Task<List<Album>> FindAlbumByName(string name)
    {
        return await _context.Albums.Where(x => x.Name.ToLower().Contains(name)).ToListAsync();
    }
    public async Task<Album> UpdateAlbum(Album album)
    {
        await _context.SaveChangesAsync();
        return album;
    }

    public async Task DeleteAlbum(TEntity album)
    {
        _dbSet.Remove(album);
        await _context.SaveChangesAsync();
    }

    public Task GetAllAlbums()
    {
        throw new NotImplementedException();
    }

    public Task<List<Album>> GetAllAlbum()
    {
        throw new NotImplementedException();
    }
}