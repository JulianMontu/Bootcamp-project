using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Business.Services
{
    public class ArtistService : IArtistService
    {
        public Task<Artist> CreateArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artist>> GetArtistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Artist> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Artist> UpdateArtist(Artist artist)
        {
            throw new NotImplementedException();
        }
    }
}
