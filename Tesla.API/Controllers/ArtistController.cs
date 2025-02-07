using Microsoft.AspNetCore.Mvc;
using Tesla.Business.Interfaces;

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
}