using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using SportGameApiTests;
using SportGamesAPI.Controllers;
using SportGamesAPI.Data.Interfaces;
using SportGamesAPI.Hubs;
using SportGamesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGameApiTests
{
    public class FakeHubContext : IHubContext<SportGameHub>
    {

        IGroupManager IHubContext<SportGameHub>.Groups => throw new NotImplementedException();

        IHubClients IHubContext<SportGameHub>.Clients => new FakeHubClients();
    }

    public class FakeHubClients : IHubClients
    {
        IClientProxy IHubClients<IClientProxy>.All => new FakeClientProxy();

        IClientProxy IHubClients<IClientProxy>.AllExcept(IReadOnlyList<string> excludedConnectionIds)
        {
            throw new NotImplementedException();
        }

        IClientProxy IHubClients<IClientProxy>.Client(string connectionId)
        {
            throw new NotImplementedException();
        }

        IClientProxy IHubClients<IClientProxy>.Clients(IReadOnlyList<string> connectionIds)
        {
            throw new NotImplementedException();
        }

        IClientProxy IHubClients<IClientProxy>.Group(string groupName)
        {
            throw new NotImplementedException();
        }

        IClientProxy IHubClients<IClientProxy>.GroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds)
        {
            throw new NotImplementedException();
        }

        IClientProxy IHubClients<IClientProxy>.Groups(IReadOnlyList<string> groupNames)
        {
            throw new NotImplementedException();
        }

        IClientProxy IHubClients<IClientProxy>.User(string userId)
        {
            throw new NotImplementedException();
        }

        IClientProxy IHubClients<IClientProxy>.Users(IReadOnlyList<string> userIds)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeClientProxy : IClientProxy
    {
        public Task SendAsync(string methodName, params object[] args)
        {
            return Task.CompletedTask;
        }

        public Task SendCoreAsync(string methodName, object[] args, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    [TestClass]
    public class SportGameControllerTests
    {
        private Mock<ISportGameRepository> _sportGameRepository;
        private FakeHubContext _hub;
        private Fixture _fixture;
        private SportGameController _controller;

        
        public SportGameControllerTests()
        {
            _sportGameRepository = new Mock<ISportGameRepository>();
            _hub = new FakeHubContext();
            _fixture = new Fixture();
            _controller = new SportGameController(_hub, _sportGameRepository.Object);
        }

        [TestMethod]
        public async Task GetSportGames_ReturnsSportGamesList()
        {
            // Arrange
            var sportGames = _fixture.CreateMany<SportGame>(20).ToList();
            _sportGameRepository.Setup(x => x.GetSportGames()).ReturnsAsync(sportGames);

            // Act
            var result = await _controller.GetSportGames();

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(List<SportGame>));
        }

        [TestMethod]
        public async Task GetSportGames_ThrowInternalError()
        {
            // Arrange
            _sportGameRepository.Setup(x => x.GetSportGames()).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetSportGames();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(StatusCodeResult));
            Assert.AreEqual(500, (result.Result as StatusCodeResult).StatusCode);
        }

        [TestMethod]
        public async Task GetSportGame_ReturnsSportGame()
        {
            // Arrange
            var sportGame = _fixture.Create<SportGame>();
            _sportGameRepository.Setup(x => x.GetSportGame(It.IsAny<int>())).ReturnsAsync(sportGame);

            // Act
            var result = await _controller.GetSportGame(1);

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(SportGame));
        }

        [TestMethod]
        public async Task GetSportGame_ThrowInternalError()
        {
            // Arrange
            _sportGameRepository.Setup(x => x.GetSportGame(It.IsAny<int>())).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetSportGame(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(StatusCodeResult));
            Assert.AreEqual(500, (result.Result as StatusCodeResult).StatusCode);
        }

        [TestMethod]
        public async Task CreateSportGame_ReturnsSportGame()
        {
            // Arrange
            var sportGame = _fixture.Create<SportGame>();
            _sportGameRepository.Setup(x => x.CreateSportGame(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(sportGame);

            var sportGames = _fixture.CreateMany<SportGame>(20).ToList();
            _sportGameRepository.Setup(x => x.GetSportGames()).ReturnsAsync(sportGames);

            //_hub.Setup(x => x.SendUpdatedGames(It.IsAny<List<SportGame>>()));


            // Act
            var result = await _controller.CreateSportGame("team1", "team2");

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(SportGame));
        }

        [TestMethod]
        public async Task CreateSportGame_ThrowInternalError()
        {
            // Arrange
            _sportGameRepository.Setup(x => x.CreateSportGame(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreateSportGame("team1", "team2");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(StatusCodeResult));
            Assert.AreEqual(500, (result.Result as StatusCodeResult).StatusCode);
        }

        [TestMethod]
        public async Task UpdateSportGame_ReturnsSportGame()
        {
            // Arrange
            var sportGame = _fixture.Create<SportGame>();
            _sportGameRepository.Setup(x => x.UpdateSportGame(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(sportGame);

            var sportGames = _fixture.CreateMany<SportGame>(20).ToList();
            _sportGameRepository.Setup(x => x.GetSportGames()).ReturnsAsync(sportGames);

            // Act
            var result = await _controller.UpdateSportGame(1, 1, 1);

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(SportGame));
        }

        [TestMethod]
        public async Task UpdateSportGame_ThrowInternalError()
        {
            // Arrange
            _sportGameRepository.Setup(x => x.UpdateSportGame(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.UpdateSportGame(1, 1, 1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(StatusCodeResult));
            Assert.AreEqual(500, (result.Result as StatusCodeResult).StatusCode);
        }

        [TestMethod]
        public async Task FinishSportGame_ReturnSportGame()
        {
            // Arrange
            var sportGame = _fixture.Create<SportGame>();
            _sportGameRepository.Setup(x => x.FinishSportGame(It.IsAny<int>())).ReturnsAsync(sportGame);

            var sportGames = _fixture.CreateMany<SportGame>(20).ToList();
            _sportGameRepository.Setup(x => x.GetSportGames()).ReturnsAsync(sportGames);

            // Act
            var result = await _controller.FinishSportGame(1);

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(SportGame));
        }

        [TestMethod]
        public async Task FinishSportGame_ThrowInternalError()
        {
            // Arrange
            _sportGameRepository.Setup(x => x.FinishSportGame(It.IsAny<int>())).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.FinishSportGame(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(StatusCodeResult));
            Assert.AreEqual(500, (result.Result as StatusCodeResult).StatusCode);

        }
    }
}
