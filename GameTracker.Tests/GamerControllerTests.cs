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
            // mockFilterOptions.Setup(x => x.SearchTerm == "Test Name").Returns(new FilterOptions());
            mockGamerService.Setup(x => x.GetGamers());
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<GamerDto>(It.IsAny<Gamer>())).Returns(new GamerDto
            {
                Id = 1,
                Name = "Test Name",
                Email = "test@mail.ru",
                GamesPlayed = 5,
                PhoneNumber = "012345678",
                Wallet = 1000
            });
            mockGamerService.Setup(x => x.GetGamerById(It.Is<int>(x => x == 1))).ReturnsAsync(new GamerDto()
                 {       
                        Id=1,
                        Name="Test Name",
                        Email="test@mail.ru",
                        GamesPlayed=5,
                        PhoneNumber="012345678",
                        Wallet=1000                                      
                 });
            mockGamerService.Setup(x => x.UpdateGamer(It.Is<int>(x => x == 1), It.IsAny<GamerCreateDto>()));

            mockGamerService.Setup(x => x.CreateGamer(It.IsAny<GamerCreateDto>())).ReturnsAsync(new GamerDto()
            {
                Id=1,
                Name = "Test Name",
                Email = "test@mail.ru",
                GamesPlayed = 5,
                PhoneNumber = "012345678",
                Wallet = 1000
            });
            mockGamerService.Setup(x => x.DeleteGamer(It.Is<int>(x => x == 1)));
            _cont = new GamersController(mockGamerService.Object, mockMapper.Object, null);
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
        [Fact]
        public  void GET_Gamers_Returns_Ok_Status()
        {
            //Arrange
            var options = new FilterOptions() {SearchTerm= "Test Name", Order=0 };
            //Act
            var result = _cont.Get()as ObjectResult;
           // var gamers = result.Value as List<Gamer>;
            //Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
           // Assert.NotNull(result);
        }
        [Fact]
        public async Task Put_Gamer_WithId_Returns_Ok_Status()
        {
            //Arrange
            var id = 1;
            var dto = new GamerCreateDto() { Name = "Name", Email = "kek@mail.com", GamesPlayed = 1, PhoneNumber = "1111111", Wallet = 100 };
            //Act
            var result = await _cont.UpdateGamer(id,dto) as OkResult;
            
            //Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
           
        }
        [Fact]
        public async Task Patch_Gamer_And_Check_FOR_nULL()
        {
            //Arrange
            var id = 1;
            var dto = new GamerCreateDto();
            //Act
            var result = await _cont.UpdateGamer(id, dto); ;
            // var gamer = result.Value as GamerDto;
            //Assert
            //Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Post_Gamer_And_Check_For_Created_StatusCode()
        {
            //Arrange
            var dto = new GamerCreateDto();
            //Act
            var result = await _cont.CreateGamer(dto) as ObjectResult;
            // var gamer = result.Value as GamerDto;
            //Assert
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
            Assert.NotNull(result);
        }
        [Fact]
        public void Delete_Gamer_WithId_Returns_Gamer()
        {
            //Arrange
            var id = 1;
            //Act
            var result =  _cont.RemoveGamer(id);
            // var gamer = result.Value as GamerDto;
            //Assert
            //Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result);
        }

    }
}
