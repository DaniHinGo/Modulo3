using System.Net;
using Tesla.Business.Interfaces;
using Tesla.Data;
using Tesla.Data.Models;

namespace Tesla.Business.Services;


public class ArtistService : IArtistService
{
    private readonly IUnitOfWork _unitOfWork;
    private List<Artist> _listaArtist = new();
    public ArtistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Artist>> AddArtist(Artist artist)
    {
        var isValid = ValidateModel(artist);
        if (!string.IsNullOrEmpty(isValid))
        {
            return BuildResponse(null, isValid, HttpStatusCode.BadRequest, new());
        }

        try
        {

            await _unitOfWork.ArtistRepository.AddAsync(artist);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            return new BaseMessage<Artist>()
            {
                Message = $"[Exception]: {ex.Message}",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new()
            };
        }


        return new BaseMessage<Artist>()
        {
            Message = "",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = 1,
            ResponseElements = new List<Artist> { artist }
        };

    }

    private string? ValidateModel(Artist artist)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseMessage<Artist>> FindById(int id)
    {
        Artist? artist = new();
        artist = await _unitOfWork.ArtistRepository.FindAsync(id);

        return artist != null ?
            BuildResponse(new List<Artist>() { artist }, "", HttpStatusCode.OK, 1) :
            BuildResponse(new List<Artist>(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>> FindByName(string name)
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return lista.Any() ? BuildResponse(lista.ToList(), "", HttpStatusCode.OK, lista.Count()) :
            BuildResponse(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>> FindByProperties(string name, int year)
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync(x => x.Name.Contains(name) && x.Name == name);
        return lista.Any() ? BuildResponse(lista.ToList(), "", HttpStatusCode.OK, lista.Count()) :
            BuildResponse(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>> GetList()
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync();
        return lista.Any() ? BuildResponse(lista.ToList(), "", HttpStatusCode.OK, lista.Count()) :
            BuildResponse(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    private BaseMessage<Artist> BuildResponse(List<Artist> lista, string message = "", HttpStatusCode status = HttpStatusCode.OK,
        int totalElements = 0)
    {
        return new BaseMessage<Artist>()
        {
            Message = message,
            StatusCode = status,
            TotalElements = totalElements,
            ResponseElements = lista
        };
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

    public async Task<string> TestArtistreation(Artist artist)
    {
        return ValidateModel(artist);
    }

    public Task<BaseMessage<Artist>> DeleteArtist(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> FindArtistByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> GetAllArtist()
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> FindArtistById(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArtist(int id, Artist artist)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    Task<Artist> IArtistService.FindById(int id)
    {
        throw new NotImplementedException();
    }

    Task<Artist> IArtistService.AddArtist(Artist artist)
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> GetArtists()
    {
        throw new NotImplementedException();
    }

    Task<BaseMessage<Artist>> IArtistService.UpdateArtist(int id, Artist artist)
    {
        throw new NotImplementedException();
    }

    Task IArtistService.GetAllArtist()
    {
        return GetAllArtist();
    }

    Task IArtistService.FindArtistByName(string name)
    {
        return FindArtistByName(name);
    }

    public Task<BaseMessage<Artist>> GetAllArtists()
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Artist>> FindArtistsByName(string name)
    {
        throw new NotImplementedException();
    }
    #endregion
}