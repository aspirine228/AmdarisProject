using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using GameTracker.API.Controllers;
using GameTracker.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using GameTracker.Common.Dtos.Gamer;
using AutoMapper;
using System.Net;
using GameTracker.Common.Models;
using GameTracker.Domain.Entities;

namespace GameTracker.Tests
{
    public class GamerControllerTests
    {
        private readonly GamersController _cont;
        public GamerControllerTests()
        {
            var mockGamerService = new Mock<IGamerService>();
            var mockFilterOptions = new Mock<FilterOptions>();
            mockGamerService.Setup(x => x.GetGamers(mockFilterOptions.Object)).Returns(new List<Gamer> { new Gamer { }, new Gamer { } });
            mockGamerService.Setup(x => x.GetGamerById(It.Is<int>(x => x == 1))).ReturnsAsync(new GamerDto()
                 {       
                        Id=1,
                        Name="Test Name",
                        Email="test@mail.ru",
                        GamesPlayed=5,
                        PhoneNumber="012345678",
                        Wallet=1000                                      
                 });

            var mockMapper = new Mock<IMapper>();
            _cont = new GamersController(mockGamerService.Object, mockMapper.Object);
        }
        [Fact]
        public async Task Get_Gamer_WithId_Returns_Gamer()
        {
            //Arrange
            var id = 1;
            //Act
            var result = await _cont.GetGamerById(id) ;
           // var gamer = result.Value as GamerDto;
            //Assert
            //Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, result.Id);
        }
        public async Task Post_Gamer_Returns_Gamer()
        {
            //Arrange
            var id = 1;
            //Act
            var result = await _cont.GetGamerById(id);
            // var gamer = result.Value as GamerDto;
            //Assert
            //Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, result.Id);
        }
        public async Task Put_Gamer_WithId_Returns_Gamer()
        {
            //Arrange
            var id = 1;
            //Act
            var result = await _cont.GetGamerById(id);
            // var gamer = result.Value as GamerDto;
            //Assert
            //Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, result.Id);
        }
        public async Task Patch_Gamer_WithId_Returns_Gamer()
        {
            //Arrange
            var id = 1;
            //Act
            var result = await _cont.GetGamerById(id);
            // var gamer = result.Value as GamerDto;
            //Assert
            //Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, result.Id);
        }
        public async Task Delete_Gamer_WithId_Returns_Gamer()
        {
            //Arrange
            var id = 1;
            //Act
            var result = await _cont.GetGamerById(id);
            // var gamer = result.Value as GamerDto;
            //Assert
            //Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, result.Id);
        }

    }
}
