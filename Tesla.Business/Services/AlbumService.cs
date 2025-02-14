using Microsoft.VisualBasic;
using Tesla.Business.Interfaces;
using Tesla.Data.Models;
using System.Net;
using Tesla.Data;

namespace Tesla.Business.Services;

public class AlbumService : IAlbumService
{
    private readonly IUnitOfWork _unitOfWork;
    private List<Album> _listaAlbum = new();
    public AlbumService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Album>> AddAlbum(Album album)
    {
        var isValid = ValidateModel(album);
        if (!string.IsNullOrEmpty(isValid))
        {
            return BuildResponse(null, isValid, HttpStatusCode.BadRequest, new());
        }

        try
        {

            await _unitOfWork.AlbumRepository.AddAsync(album);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            return new BaseMessage<Album>()
            {
                Message = $"[Exception]: {ex.Message}",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new()
            };
        }


        return new BaseMessage<Album>()
        {
            Message = "",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = 1,
            ResponseElements = new List<Album> { album }
        };

    }

    public async Task<BaseMessage<Album>> FindById(int id)
    {
        Album? album = new();
        album = await _unitOfWork.AlbumRepository.FindAsync(id);

        return album != null ?
            BuildResponse(new List<Album>() { album }, "", HttpStatusCode.OK, 1) :
            BuildResponse(new List<Album>(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByName(string name)
    {
        var lista = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return lista.Any() ? BuildResponse(lista.ToList(), "", HttpStatusCode.OK, lista.Count()) :
            BuildResponse(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindByProperties(string name, int year)
    {
        var lista = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Name.Contains(name) && x.Year == year);
        return lista.Any() ? BuildResponse(lista.ToList(), "", HttpStatusCode.OK, lista.Count()) :
            BuildResponse(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> GetList()
    {
        var lista = await _unitOfWork.AlbumRepository.GetAllAsync();
        return lista.Any() ? BuildResponse(lista.ToList(), "", HttpStatusCode.OK, lista.Count()) :
            BuildResponse(lista.ToList(), "", HttpStatusCode.NotFound, 0);
    }

    private BaseMessage<Album> BuildResponse(List<Album> lista, string message = "", HttpStatusCode status = HttpStatusCode.OK,
        int totalElements = 0)
    {
        return new BaseMessage<Album>()
        {
            Message = message,
            StatusCode = status,
            TotalElements = totalElements,
            ResponseElements = lista
        };
    }

    private string ValidateModel(Album album)
    {
        string message = string.Empty;

        if (string.IsNullOrEmpty(album.Name))
        {
            message += "El nombre es requerido";
        }
        if (album.Year < 1901 || album.Year > DateAndTime.Now.Year)
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

    public Task<BaseMessage<Album>> DeleteAlbum(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Album>> FindAlbumByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Album>> GetAllAlbums()
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<Album>> FindAlbumById(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAlbum(int id, Album album)
    {
        throw new NotImplementedException();
    }

    public Task DeleteById(int id)
    {
        throw new NotImplementedException();
    }
    #endregion
}