using Microsoft.VisualStudio.TestTools.UnitTesting;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using Discoteque.Data.IRepositories;
using Discoteque.Data;
using Discoteque.Business.Services;
using NSubstitute.Core;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Discoteque.Tests;

[TestClass]
public class AlbumTests
{
    private readonly IRepository<int, Album> _albumRepository;
    private readonly IRepository<int, Artist> _artistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAlbumService _albumServices;
    private readonly Album _correctAlbum;
    private readonly Album _wrongAlbum;
    private const string ALMBU_SERVICE_EXCEPTION = "Album Exception Thrown";

    public AlbumTests()
    {
        _albumRepository = Substitute.For<IRepository<int, Album>>();
        _artistRepository = Substitute.For<IRepository<int, Artist>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _albumServices = new AlbumService(_unitOfWork);
        _correctAlbum = new Album()
        {
            Name = "Midnight sun",
            ArtistId = 1,
            Cost = 60,
            Genre = Genres.Rock,
            Year = 1985
        };
        _wrongAlbum = new Album()
        {
            Name = "Midnight sun",
            ArtistId = 1,
            Cost = 60,
            Genre = Genres.Rock,
            Year = 1900
        };
    }

    [TestMethod]
    public async Task IsAlbumCreatedCorrectly()
    {
        //Arrange preparar lo que me interesa probar
        _artistRepository.FindAsync(1).Returns(Task.FromResult(new Artist()));
        _albumRepository.AddAsync(_correctAlbum).Returns(Task.FromResult(_correctAlbum));
        _unitOfWork.AlbumRepository.Returns(_albumRepository);
        _unitOfWork.ArtistRepository.Returns(_artistRepository);

        //Act donde y como va a ser
        var newAlbum = await _albumServices.CreateAlbum(_correctAlbum);


        //Assert
        Assert.AreEqual(newAlbum.StatusCode, System.Net.HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task ShouldWrongAlbumReturningBadRequest()
    {
        _artistRepository.FindAsync(1).Returns(Task.FromResult(new Artist()));
        _albumRepository.AddAsync(_correctAlbum).Returns(Task.FromResult(_correctAlbum));
        _unitOfWork.AlbumRepository.Returns(_albumRepository);
        _unitOfWork.ArtistRepository.Returns(_artistRepository);

        //act
        var newAlbum = await _albumServices.CreateAlbum(_wrongAlbum);

        Assert.AreEqual(newAlbum.StatusCode, System.Net.HttpStatusCode.BadRequest);

    }

    [TestMethod]
    public async Task IsExceptionHandled()
    {
        _artistRepository.FindAsync(1).Returns(Task.FromResult(new Artist()));
        _albumRepository.AddAsync(_correctAlbum).ThrowsAsyncForAnyArgs(new Exception(ALMBU_SERVICE_EXCEPTION));
        _unitOfWork.AlbumRepository.Returns(_albumRepository);
        _unitOfWork.ArtistRepository.Returns(_artistRepository);

        //act
        var newAlbum = await _albumServices.CreateAlbum(_correctAlbum);

        Assert.IsTrue(newAlbum.Message.Contains(ALMBU_SERVICE_EXCEPTION));

    }
}
