using Discoteque.Data.Dto;
using Discoteque.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discoteque.Business.IServices
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetToursAsync();
        Task<Tour> GetTourById(int id);
        Task<IEnumerable<Tour>> GetToursByArtist(int ArtistId);
        Task<IEnumerable<Tour>> GetToursByYear(int year);
        Task<IEnumerable<Tour>> GetToursByCity(string city);
        Task<BaseMessage<Tour>> CreateTour(Tour tour);
        Task<Tour> UpdateTour(Tour tour);
    }
}
