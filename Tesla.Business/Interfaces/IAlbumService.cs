using Tesla.Data.Models;

namespace Tesla.Business.Interfaces;



public interface IAlbumService
{
    Task<BaseMessage<Album>> GetList();
    Task<BaseMessage<Album>> AddAlbum(Album album);
    Task<BaseMessage<Album>> FindById(int id);
    Task<BaseMessage<Album>> FindByName(string name);
    Task<BaseMessage<Album>> FindByProperties(string name, int year);
    Task<BaseMessage<Album>> DeleteAlbum(int id);
    Task<BaseMessage<Album>> FindAlbumByName(string name);
    Task<BaseMessage<Album>> GetAllAlbums();
    Task<BaseMessage<Album>> FindAlbumById(int id);
    

    #region Learning to Test
    Task<string> HealthCheckTest();
    Task<string> TestAlbumCreation(Album album);
    Task UpdateAlbum(int id, Album album);
    Task DeleteById(int id);

    #endregion
}