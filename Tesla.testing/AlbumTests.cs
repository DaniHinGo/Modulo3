using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tesla.Business.Services;
using Tesla.Data.IRepository;
using Tesla.Data.Models;
using Tesla.Data.Repository;

namespace Tesla.Tests;

[TestClass]
public class AlbumTests
{
    private IAlbumRepository<int, Album> _albumRepository;
    private readonly Album _correctAlbum;
    public AlbumTests()
    {
        _albumRepository = Substitute.For<IAlbumRepository<int, Album>>();
        _correctAlbum = new Album(){
            Id = 1,
        };
    }

     /* 
        Patrón AAA
        A = Arrange
        A = Act
        A = Assert
     */

    [TestMethod]
    public async Task HealthCheckTest()
    {
        // Arrange
        var service = new AlbumService(null);

        // Act
        var response = await service.HealthCheckTest();

        // Assert
        Assert.AreEqual(response, "OK");
    }

    [TestMethod]
    public async Task HealthCheckTestIsOk()
    {
        // Arrange
        var service = new AlbumService(null);

        // Act
        var response = await service.HealthCheckTest(true);

        // Assert
        Assert.AreEqual(response, "OK!");
    }

    [TestMethod]
    public async Task HealthCheckTestIsNotOkay()
    {
        // Arrange
        var service = new AlbumService(null);

        // Act
        var response = await service.HealthCheckTest(false);

        // Assert
        Assert.AreEqual(response, "Not cool");
    }

    [TestMethod]
    public async Task ValidateAlbumCreation()
    {
        // Arrange
        var service = new AlbumService(null);
        var album = new Album(){
            Name =  "Y ahora qué?",
            Year = 2001,
            ArtistId = 1,
            Genre = Genre.Punk,
            Id = 1
        };

        // Act
        var response = await service.TestAlbumCreation(album);

        //Assert
        Assert.AreEqual(response, String.Empty);

    }

    [TestMethod]
    public async Task ValidateAlbumCreation_nameisempty()
    {
        // Arrange
        var service = new AlbumService(null);
        var album = new Album(){
            Name =  "",
            Year = 2025,
            ArtistId = 1,
            Genre = Genre.Rock,
            Id = 1
        };

        // Act
        var response = await service.TestAlbumCreation(album);

        //Assert
        Assert.IsTrue(response.Contains("El nombre es requerido"));

    }

    [TestMethod]
    public async Task ValidateAlbumCreation_yearoutofrangelower()
    {
        // Arrange
        var service = new AlbumService(null);
        var album = new Album(){
            Name =  "",
            Year = 1899,
            ArtistId = 1,
            Genre = Genre.Rock,
            Id = 1
        };

        // Act
        var response = await service.TestAlbumCreation(album);

        //Assert
        Assert.IsTrue(response.Contains("El año del disco debe estar entre 1901 y 2025"));

    }

    [TestMethod]
    public async Task ValidateAlbumCreation_yearoutofrangehigher()
    {
        // Arrange
        var service = new AlbumService(null);
        var album = new Album(){
            Name =  "",
            Year = 2026,
            ArtistId = 1,
            Genre = Genre.Rock,
            Id = 1
        };

        // Act
        var response = await service.TestAlbumCreation(album);

        //Assert
        Assert.IsTrue(response.Contains("El año del disco debe estar entre 1901 y 2025"));
    }

    [TestMethod]
    public async Task FindById_FindsSomething()
    {
        // Arrange
        //MOCKING 
        _albumRepository.FindAsync(1).ReturnsForAnyArgs(Task.FromResult<Album>(_correctAlbum));
        var service = new AlbumService(_albumRepository);

        //Act
        var result = await service.FindById(5);
        //Assert
        Assert.AreEqual(result.TotalElements, 1);
    }

    [TestMethod]
    public async Task FindById_NotFound()
    {
        // Arrange
        //MOCKING 
        _albumRepository.FindAsync(1).ReturnsForAnyArgs(Task.FromResult<Album>(null));
        var service = new AlbumService(_albumRepository);

        //Act
        var result = await service.FindById(5);
        //Assert
        //Assert.AreEqual(result.StatusCode == HttpStatusCode.NotFound);
        Assert.IsTrue(result.TotalElements == 0 && result.StatusCode == HttpStatusCode.NotFound);
    }
    // [TestMethod]
    // public async Task FindById_ThrowException()
    // {
    //     // Arrange
    //     // MOCKING
    //     _albumRepository.FindAsync(1).ThrowsAsyncForAnyArgs(new Exception("ERROR"));
    //     var service = new AlbumService(_albumRepository);

    //     // Act
    //     //var result = await service.FindById(5);

    //     // Assert
    //     //Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound);
    //     Assert.ThrowsException<Exception>(async () => await service.FindById(5));
    // }

    [TestMethod]
    public async Task AddAlbum_NameIsIncorrect()
    {
        // Arrange
        // MOCKING
        _albumRepository.AddAsync(new Album());
        var service = new AlbumService(_albumRepository);

        // Act
        var result = await service.AddAlbum(_correctAlbum);

        // Assert
        Assert.AreEqual(result.TotalElements, 0);
    }


}