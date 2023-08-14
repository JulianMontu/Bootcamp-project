using Discoteque.Business.Utils;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;
using Discoteque.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discoteque.Business.IServices;

namespace Discoteque.Business.Services
{
    public class TourService : ITourService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TourService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseMessage<Tour>> CreateTour(Tour tour)
        {
            try
            {
                var artist = await _unitOfWork.ArtistRepository.FindAsync(tour.ArtistId);
                if (tour.TourDate.Year <= 2021 || artist == null)
                {
                    return Utilities.BuildResponse<Tour>(HttpStatusCode.NotFound, BaseMessageStatus.BAD_REQUEST_400);
                }

                await _unitOfWork.TourRepository.AddAsync(tour);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return Utilities.BuildResponse<Tour>(HttpStatusCode.InternalServerError, BaseMessageStatus.INTERNAL_SERVER_ERROR_500);
            }

            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Tour>() { tour });
        }

        public async Task<Tour> GetTourById(int id)
        {
            return await _unitOfWork.TourRepository.FindAsync(id);
        }

        public async Task<IEnumerable<Tour>> GetToursAsync()
        {
            return await _unitOfWork.TourRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Tour>> GetToursByArtist(int artistId)
        {
            return await _unitOfWork.TourRepository.GetAllAsync(x => x.ArtistId == artistId, includeProperties: new Artist().GetType().Name);
        }

        public async Task<IEnumerable<Tour>> GetToursByCity(string city)
        {
            return await _unitOfWork.TourRepository.GetAllAsync(x => x.Equals(city));
        }

        public async Task<IEnumerable<Tour>> GetToursByYear(int year)
        {
            return await _unitOfWork.TourRepository.GetAllAsync(x => x.TourDate.Year == year);
        }

        public async Task<Tour> UpdateTour(Tour tour)
        {
            await _unitOfWork.TourRepository.Update(tour);
            await _unitOfWork.SaveAsync();
            return tour;
        }
    }
}
