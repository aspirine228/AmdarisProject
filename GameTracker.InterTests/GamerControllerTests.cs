
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GameTracker.API;
using Xunit;
using System.Web;
using GameTracker.Common.Dtos.Gamer;
using System.Net.Http;
using Newtonsoft.Json;
using GameTracker.Domain.Entities;
using Moq;
using System.Net.Http.Json;

namespace GameTracker.InterTests
{
    public class GamerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        

        public GamerControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
          
            
        }
        [Fact]
        public async Task Get_WithId_Returns_Ok_StatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/api/gamers/1";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        [Fact]
        public async Task Get_Returns_Ok_StatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/api/gamers?SearchTerm=string&Order=0";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        [Fact]
        public async Task Post_Returns_Ok_StatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/api/gamers";
            var gamer = new GamerCreateDto()
            {
              
                Name = "Test Name",
                Email = "test@mail.ru",
                GamesPlayed = 5,
                PhoneNumber = "012345678",
                Wallet = 1000
            };
          
            // Act
            var response = await client.PostAsJsonAsync(url, gamer);
            
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        [Fact]
        public async Task Put_Returns_Ok_StatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/api/gamers/2002";
            var gamer = new GamerCreateDto()
            {
               
                Name = "Test Name",
                Email = "tester@mail.ru",
                GamesPlayed = 5,
                PhoneNumber = "012345678",
                Wallet = 1000
            };
            var cont = JsonConvert.SerializeObject(gamer);
            var stringContent = new StringContent(cont);
            // Act
            var response = await client.PutAsync(url,stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
           
        }
        [Fact]
        public async Task Patch_Returns_Ok_StatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/api/gamers/1003";
            var gamer = new GamerCreateDto()
            {

                Name = "Test Name",
                Email = "tester@mail.ru",
                GamesPlayed = 5,
                PhoneNumber = "012345678",
                Wallet = 1000
            };
            var cont = JsonConvert.SerializeObject(gamer);
            var buffer = System.Text.Encoding.UTF8.GetBytes(cont);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            // Act
            var response = await client.PatchAsync(url, byteContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        [Fact]
        public async Task Delete_Returns_Ok_StatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/api/gamers/1003";

            // Act
            var response = await client.DeleteAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
