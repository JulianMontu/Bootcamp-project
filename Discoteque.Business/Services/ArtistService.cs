using Discoteque.Business.IServices;
using Discoteque.Business.Utils;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Business.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseMessage<Artist>> CreateArtist(Artist artist)
        {
            try
            {
                if (artist.Name.Length > 100)
                {
                    return Utilities.BuildResponse<Artist>(HttpStatusCode.BadRequest,BaseMessageStatus.BAD_REQUEST_400);
                }

                await _unitOfWork.ArtistRepository.AddAsync(artist);
                await _unitOfWork.SaveAsync();
                
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Artist>(HttpStatusCode.BadRequest, BaseMessageStatus.INTERNAL_SERVER_ERROR_500);
            }
            return Utilities.BuildResponse<Artist>(HttpStatusCode.OK,BaseMessageStatus.OK_200,new List<Artist>() { artist});
        }

        public async Task<BaseMessage<Artist>> CreateArtistsInBatch(List<Artist> artists)
        {
            try
            {
                foreach (var item in artists)
                {
                    if (item.Name.Length <= 100)
                    {
                        await _unitOfWork.ArtistRepository.AddAsync(item);
                    }
                }
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Artist>(HttpStatusCode.BadRequest, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }

            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, artists);
        }

        public async Task<IEnumerable<Artist>> GetArtistsAsync()
        {
            return await _unitOfWork.ArtistRepository.GetAllAsync();
        }

        public async Task<Artist> GetById(int id)
        {
            return await _unitOfWork.ArtistRepository.FindAsync(id);
        }

        public async Task<Artist> UpdateArtist(Artist artist)
        {
            await _unitOfWork.ArtistRepository.Update(artist);
            await _unitOfWork.SaveAsync();
            return artist;
        }
    }
}
