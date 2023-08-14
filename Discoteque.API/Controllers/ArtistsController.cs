using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Business.Services;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [Route("GetAllArtistsAsync")]
        public async Task<IActionResult> GetArtistsAsync()
        {
            var artist = await _artistService.GetArtistsAsync();
            return Ok(artist);
        }

        [HttpPost]
        [Route("CreateArtistAsync")]
        public async Task<IActionResult> CreateArtistAsync(Artist artist)
        {
            var result = await _artistService.CreateArtist(artist);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateArtists")]
        public async Task<IActionResult> CreateArtistsAsync(List<Artist> artists)
        {
            var result = await _artistService.CreateArtistsInBatch(artists);
            return result.StatusCode == HttpStatusCode.OK ? Ok(result) : StatusCode((int)result.StatusCode, result);
        }

        [HttpPatch]
        [Route("UpdateArtistAsync")]
        public async Task<IActionResult> UpdateArtistAsync(Artist artist)
        {
            return Ok();
        }
    }
}
