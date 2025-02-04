using Microsoft.VisualBasic;
using Tesla.Business.Interfaces;
using Tesla.Data.Models;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Tesla.Business.Services;

public class AlbumService : IAlbumService
{
    private List<Album> _listaAlbum = new();
    public AlbumService()
    {
       _listaAlbum.Add(new ()
       {
            Name = "Ni un paso atrás",
            Gender = Gender.Punk_rock,
            Year = 1991,
            Id = 1,
            Artist = new() { Id = 0, Name = "Reincidentes", Label = "Discos Suicidas", IsOnTour = false }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Dejenme ser",
            Gender = Gender.Rock,
            Year = 2002,
            Id = 2,
            Artist = new() { Id = 0, Name = "Código Rojo", Label = "Universal Music", IsOnTour = false }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Con todo respeto",
            Gender = Gender.Rock,
            Year = 2004,
            Id = 3,
            Artist = new() { Id = 0, Name = "Molotov", Label = "Universal", IsOnTour = false }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Poetics",
            Gender = Gender.Rock_alternatico,
            Year = 2009,
            Id = 4,
            Artist = new() { Id = 0, Name = "Panda", Label = "Movic Records", IsOnTour = true }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Finisterra",
            Gender = Gender.Folk_Metal,
            Year = 2000,
            Id = 5,
            Artist = new() { Id = 0, Name = "Mago de Oz", Label = "	Locomotive Music", IsOnTour = true }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Viaje por lo eterno",
            Gender = Gender.Rock,
            Year = 2014,
            Id = 6,
            Artist = new() { Id = 0, Name = "Reyno", Label = "Universal Music", IsOnTour = true }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Piel de cobre",
            Gender = Gender.Hard_rock,
            Year = 1993,
            Id = 7,
            Artist = new() { Id = 0, Name = "Kraken", Label = "Discos fuentes", IsOnTour = false }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Firmes",
            Gender = Gender.Punk,
            Year = 2009,
            Id = 8,
            Artist = new() { Id = 0, Name = "I.R.A", Label = "Universal Music", IsOnTour = false }
       });
       _listaAlbum.Add(new ()
       {
            Name = "Navegantes",
            Gender = Gender.Synthpop,
            Year = 2019,
            Id = 9,
            Artist = new() { Id = 0, Name = "Camilo Séptimo", Label = "	Warner Chappell Music", IsOnTour = true }
       });       
    }
    public async Task<BaseMessage<Album>> AddAlbum()
    {
        try{
            _listaAlbum.Add(new (){Name = "Sunrise Over Rigor Mortis", Gender = Gender.Rock, Year = 2024, Id = 4});
        }catch{
            return new BaseMessage<Album>() {
                Message = "",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new ()
            };
        }
        
        
        return new BaseMessage<Album>() {
            Message = "",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = _listaAlbum.Count,
            ResponseElements = _listaAlbum
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
        return new BaseMessage<Album>() {
            Message = "",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = _listaAlbum.Count,
            ResponseElements = _listaAlbum
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
}