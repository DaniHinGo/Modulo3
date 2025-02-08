using Microsoft.VisualBasic;
using Tesla.Business.Interfaces;
using Tesla.Data.Models;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using Tesla.Data.IRepository;
using Tesla.Data.Repository;
using Npgsql;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Tesla.Business.Services;

public class AlbumService : IAlbumService
{
    private IAlbumRepository<int, Album> _albumRepository;
    private List<Album> _listaAlbum = new();
    public AlbumService(IAlbumRepository<int, Album> albumRepository)
    {
        _albumRepository = albumRepository;
        //_albumRepository = new AlbumRepository<int, Album>(_context);
    }

    public async Task<BaseMessage<Album>> AddAlbum(Album album)
    {
        var isValid = ValidateModel(album);
        if(string.IsNullOrEmpty(isValid))
        {
            return BuildResponse(null, isValid, HttpStatusCode.BadRequest, new());
        }

        try{
            //var result = await _alb
            await _albumRepository.AddAsync(album);
        }
        catch(Exception ex)
        {
            return new BaseMessage<Album>() {
                Message = $"[Exception]: {ex.Message}",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new ()
            };
        }
        
        
        return new BaseMessage<Album>() {
            Message = "",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = 1,
            ResponseElements = new List<Album>{album}
        };
        
    }

    public async Task<BaseMessage<Album>> FindById(int id)
    {
        //Album album;

        // foreach (var item in _listaAlbum)
        // {
        //     if(item.Id == id)
        //     {
        //         album = item;
        //     }
        // }

        // LINQ -> ORM Entity Framework
        // Dapper -> ORM (Framework Distinto)
        // IEnumerable
        // Select * from Album WHERE Id == 1 AND Nombre == Shakira || Producido < COLIOMBIA
        var lista = _listaAlbum.Where(x => x.Id == id).ToList();
        
        return lista.Any() ?  BuildResponse(lista, "", HttpStatusCode.OK, lista.Count) : 
            BuildResponse(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByName(string name)
    {
        var lista = _listaAlbum.FindAll(x => x.Name.ToLower().Contains(name.ToLower()));
        // x.Name.Include(name.ToLower())
        
        return lista.Any() ?  BuildResponse(lista, "", HttpStatusCode.OK, lista.Count) : 
            BuildResponse(lista, "", HttpStatusCode.NotFound, 0);
    }

    public Task<BaseMessage<Album>> FindByProperties(string name, int year)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseMessage<Album>> GetList()
    {
        var lista = await _albumRepository.GetAllAsync();

        foreach (var item in lista)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.Year);
            Console.WriteLine(item.Genre);
            Console.WriteLine(item.ArtistId);
            Console.WriteLine(item.Artist?.Name);

        }
        return new BaseMessage<Album>() {
            Message = "",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = lista.Count(),
            ResponseElements = lista.ToList()
        };
    }

   

    private BaseMessage<Album> BuildResponse(List<Album> lista, string message = "", HttpStatusCode status = HttpStatusCode.OK, 
        int totalElements = 0)
    {
        return new BaseMessage<Album>(){
            Message = message,
            StatusCode = status,
            TotalElements = totalElements,
            ResponseElements = lista
        };
    }

    private string ValidateModel(Album album){
        string message = string.Empty;

        if(string.IsNullOrEmpty(album.Name))
        {
            message += "El nombre es requerido";
        }
        if(album.Year < 1901 || album.Year > DateAndTime.Now.Year)
        {
            message += "El año del disco debe estar entre 1901 y 2025";
        }

        return message;
    }

#region Learning to TEst
    public async Task<string> HealthCheckTest()
    {
        return "OK";
    }

    public async Task<string> HealthCheckTest(bool IsOK)
    {
        return IsOK ? "OK!" : "Not cool";
    }

    public async Task<string> TestAlbumCreation(Album album)
    {
        return ValidateModel(album);
    }
#endregion


}