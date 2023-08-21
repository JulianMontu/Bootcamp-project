using Discoteque.Business.IServices;
using Discoteque.Data.IRepositories;
using Discoteque.Data.Models;
using Discoteque.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Discoteque.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Discoteque.Test
{
    [TestClass]
    public class TourTest
    {
        private readonly IRepository<int, Tour> _tourRepository;
        private readonly IRepository<int, Artist> _artistRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourService _tourService;
        private readonly Tour _correctTour;
        private readonly Tour _wrongTour;

        public TourTest()
        {
            _tourRepository = Substitute.For<IRepository<int, Tour>>();
            _artistRepository = Substitute.For<IRepository<int, Artist>>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _tourService = new TourService(_unitOfWork);
            _correctTour = new Tour()
            {
                Name = "Test",
                City = "Pasto",
                TourDate = DateTime.Now,
                IsSoldOut = true,
                ArtistId = 1
            };
            _wrongTour = new Tour()
            {
                Name = "Test",
                City = "Pasto",
                TourDate = new DateTime(2020, 08, 15),
                IsSoldOut = true,
                ArtistId = 1
            };
        }

        [TestMethod]
        public async Task IsTourCreatedCorrectly()
        {
            //Arrange preparar lo que me interesa probar
            _artistRepository.FindAsync(1).Returns(Task.FromResult(new Artist()));
            _tourRepository.AddAsync(_correctTour).Returns(Task.FromResult(_correctTour));
            _unitOfWork.TourRepository.Returns(_tourRepository);
            _unitOfWork.ArtistRepository.Returns(_artistRepository);

            //Act donde y como va a ser
            var newTour = await _tourService.CreateTour(_correctTour);


            //Assert
            Assert.AreEqual(newTour.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task ShouldWrongTourReturningBadRequest()
        {
            //Arrange preparar lo que me interesa probar
            _artistRepository.FindAsync(1).Returns(Task.FromResult(new Artist()));
            _tourRepository.AddAsync(_correctTour).Returns(Task.FromResult(_correctTour));
            _unitOfWork.TourRepository.Returns(_tourRepository);
            _unitOfWork.ArtistRepository.Returns(_artistRepository);

            //Act donde y como va a ser
            var newTour = await _tourService.CreateTour(_wrongTour);


            //Assert
            Assert.AreEqual(newTour.StatusCode, System.Net.HttpStatusCode.NotFound);
        }
    }
}
