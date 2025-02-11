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
    internal DbSet<TEntity> _dbset;

    public AlbumRepository(NikolaContext context)
    {
        _context = context;
        _dbset = context.Set<TEntity>();
    }
    
    
    public virtual async Task AddAsync(TEntity album)
    {
        
        await _dbset.AddAsync(album);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> FindAsync(TId id)
    {
        //var value = await _dbset.FindAsync(id);
        try
        {
            //var value = await _context.Artists.FindAsync(id);    
            var response = await _dbset.FindAsync(id);
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
}