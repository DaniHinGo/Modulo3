namespace Tesla.Controllers;

using Microsoft.AspNetCore.Mvc;
using Tesla.Business.Interfaces;
using Tesla.Business.Services;
using Tesla.Data.Models;
using System.Net;

[ApiController] // @Controller
[Route("api/[controller]")]
public class TeslaController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public TeslaController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet]
    [Route("getalbums")]
    public async Task<IActionResult> GetAllAlbums()
    {
        return Ok(await _albumService.GetList());
    }

    [HttpGet]
    [Route("GetAlbumById")]
    public async Task<IActionResult> GetAlbumById(int id)
    {
        var response = await _albumService.FindById(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("GetAlbumByName")]
    public async Task<IActionResult> GetAlbumByName(string name)
    {
        var response = await _albumService.FindByName(name);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Route("GetAlbumByNameYear")]
    public async Task<IActionResult> GetAlbumByNames(string name, int year)
    {
        var response = await _albumService.FindByName(name);
        return StatusCode((int)response.StatusCode, response);
    }
}