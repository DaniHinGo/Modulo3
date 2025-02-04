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
    internal DbSet<TEntity> _dbset;

    public ArtistRepository(NikolaContext context)
    {
        _context = context;
        _dbset = context.Set<TEntity>();
    }
    
    
    public virtual async Task AddAsync(TEntity artista)
    {
        
        await _dbset.AddAsync(artista);
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
}