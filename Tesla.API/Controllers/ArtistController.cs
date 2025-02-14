using Microsoft.AspNetCore.Mvc;
using Tesla.Business.Interfaces;
using Tesla.Data.Models;

namespace Tesla.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var artist = await _artistService.FindById(id);
        return Ok(artist);
    }

    [HttpGet]
    [Route("GetAllArtist")]
    public async Task<IActionResult> GetAllArtist()
    {
        var artist = await _artistService.GetAllArtists();
        return Ok(artist);
    }

    [HttpGet]
    [Route("GetByName")]
    public async Task<IActionResult> FindArtistByName(string name)
    {
        var artist = await _artistService.FindArtistsByName(name);
        return Ok(artist);
    }

    [HttpPost]
    [Route("CreateArtist")]
    public async Task<IActionResult> AddArtist([FromBody] Artist artist)
    {
        var newArtist = await _artistService.AddArtist(artist);
        return Ok(newArtist);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteArtist(int id)
    {
        await _artistService.DeleteArtist(id);
        return NoContent();
    }
    
}