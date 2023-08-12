using Discoteque.Data.Dto;
using Discoteque.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Business.IServices
{
    public interface IArtistService
    {
        Task<IEnumerable<Artist>> GetArtistsAsync();
        Task<Artist> GetById(int id);
        Task<BaseMessage<Artist>> CreateArtist(Artist artist);
        Task<BaseMessage<Artist>> CreateArtistsInBatch(List<Artist> artists);
        Task<Artist> UpdateArtist(Artist artist);
    }
}
