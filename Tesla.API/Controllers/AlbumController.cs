using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tesla.Business.Interfaces;
using Tesla.Data.Models;

namespace TeslaACDC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
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