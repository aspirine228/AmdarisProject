using AutoMapper;
using GameTracker.Common.Dtos.Gamer;
using GameTracker.Domain.Entities;
using GameTracker.Rep.Interfaces;
using GameTracker.Services.Interfaces;
using GameTracker.Services.Services;
using Moq;

using System;
using System.Threading.Tasks;

using Xunit;

namespace GameTracker.Tests
{
    public class GamerServiceTest
    {
        private readonly IGamerService _serv;
       
        private GamerCreateDto _testDto2 = new GamerCreateDto() { Name = "Alex", Email = "test@gmail.com", GamesPlayed = 4, PhoneNumber = "1111111", Wallet = 1000 };
        

        public GamerServiceTest()
        {


            var mockIRepository = new Mock<IRepository<Gamer>>();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Gamer>(It.IsAny<GamerCreateDto>())).Returns(new Gamer { Id = 100, Name = "Alex", Email = "test@gmail.com", GamesPlayed = 4, PhoneNumber = "1111111", Wallet = 1000 });
          //  mockIRepository.Setup(x => x.Add(It.IsAny<Gamer>())).Returns(new Gamer { });
            mockMapper.Setup(x => x.Map<GamerDto>(It.IsAny<Gamer>())).Returns(new GamerDto { Id = 100, Name = "Alex", Email = "test@gmail.com", GamesPlayed = 4, PhoneNumber = "1111111", Wallet = 1000 });
            _serv = new GamerService(mockIRepository.Object, mockMapper.Object);
        }
        [Fact]
        public async Task Create_Gamer_Then_MapIt_And_Check_From_Db()
        {
            //Arrange
            var gamer = _testDto2;
           
            //Act
            var result = await _serv.CreateGamer(gamer);
         
            //Assert
           
            Assert.Equal(gamer.Email, result.Email);
           
        }

       
    }
}
