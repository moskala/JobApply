using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JobApply.IntegrationTests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<JobApply.Startup>>
    {
        private readonly WebApplicationFactory<JobApply.Startup> _factory;

        public BasicTests(WebApplicationFactory<JobApply.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/JobOffers/Index")]
        [InlineData("/Home/About")]
        [InlineData("/JobApplications/Index")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
