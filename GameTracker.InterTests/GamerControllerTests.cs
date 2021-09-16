
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GameTracker.API;
using Xunit;
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
        public async Task Get_Returns_Ok_StatusCode()
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
    }
}
