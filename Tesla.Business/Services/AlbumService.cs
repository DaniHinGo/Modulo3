using System.Net;
using Tesla.Business.Interfaces;
using Tesla.Data.Models;

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
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Dejenme ser",
            Gender = Gender.Rock,
            Year = 2002,
            Id = 2,
            Artist = new() { Id = 0, Name = "Código Rojo", Label = "Universal Music", IsOnTour = false }
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Con todo respeto",
            Gender = Gender.Rock,
            Year = 2004,
            Id = 3,
            Artist = new() { Id = 0, Name = "Molotov", Label = "Universal", IsOnTour = false }
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Poetics",
            Gender = Gender.Rock_alternatico,
            Year = 2009,
            Id = 4,
            Artist = new() { Id = 0, Name = "Panda", Label = "Movic Records", IsOnTour = true }
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Finisterra",
            Gender = Gender.Folk_Metal,
            Year = 2000,
            Id = 5,
            Artist = new() { Id = 0, Name = "Mago de Oz", Label = "	Locomotive Music", IsOnTour = true }
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Viaje por lo eterno",
            Gender = Gender.Rock,
            Year = 2014,
            Id = 6,
            Artist = new() { Id = 0, Name = "Reyno", Label = "Universal Music", IsOnTour = true }
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Piel de cobre",
            Gender = Gender.Hard_rock,
            Year = 1993,
            Id = 7,
            Artist = new() { Id = 0, Name = "Kraken", Label = "Discos fuentes", IsOnTour = false }
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Firmes",
            Gender = Gender.Punk,
            Year = 2009,
            Id = 8,
            Artist = new() { Id = 0, Name = "I.R.A", Label = "Universal Music", IsOnTour = false }
       }
       );
       _listaAlbum.Add(new ()
       {
            Name = "Navegantes",
            Gender = Gender.Synthpop,
            Year = 2019,
            Id = 9,
            Artist = new() { Id = 0, Name = "Camilo Séptimo", Label = "	Warner Chappell Music", IsOnTour = true }
       });

       Album album = new Album();
    }
    public async Task<BaseMessage<Album>> GetAlbums()
    {
        return BuildMessage(_listaAlbum, "", HttpStatusCode.OK, _listaAlbum.Count);
    }

    public async Task<BaseMessage<Album>> FindById(int id)
    {
        var lista = _listaAlbum.Where(album => album.Id == id).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByName(string name)
    {
        var lista = _listaAlbum.Where(album => album.Name.ToLower().Contains(name.ToLower())).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByNameArtist(string artist)
    {
        var lista = _listaAlbum.Where(album => album.Artist.Name.ToLower().Contains(artist.ToLower())).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByRangeYear(int year1, int year2)
    {
        var lista = _listaAlbum.Where(album => album.Year >= year1 && album.Year <= year2).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByYear(int year)
    {
        var lista = _listaAlbum.Where(album => album.Year == year).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
        BuildMessage(lista, "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByGender(int gender)
    {

        var lista = _listaAlbum.Where(album => album.Gender == (Gender)gender).ToList();
        return lista.Any() ? BuildMessage(lista, "", HttpStatusCode.OK, lista.Count) :
            BuildMessage(lista, "Género no encontrado", HttpStatusCode.NotFound, 0);
    }

    private BaseMessage<Album> BuildMessage(List<Album> responseElements, string message = "", HttpStatusCode
    statusCode = HttpStatusCode.OK, int totalElements = 0)
    {
        return new BaseMessage<Album>()
        {
            Message = message,
            StatusCode = statusCode,
            TotalElements = totalElements,
            ResponseElements = responseElements
        };
    }

    public Task GetList()
    {
        throw new NotImplementedException();
    }
}