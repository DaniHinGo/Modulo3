using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tesla.Business.Interfaces;
using Tesla.Business.Services;
using Tesla.Data.Models;

namespace Tesla.API.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = (AlbumService?)albumService;
        }
        [HttpGet]
        [Route("GetAllAlbums")]
        public async Task<IActionResult> GetAllAlbums()
        {
            var albums = await _albumService.GetList();
            return Ok(albums);
        }

        [HttpPost]
        [Route("AddAlbum")]
        public async Task<IActionResult> AddAlbum(Album album)
        {
            var result = await _albumService.AddAlbum(album);
            return Ok(result);
        }
    }
}