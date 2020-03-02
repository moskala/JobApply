using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JobApply.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

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
        [InlineData("/Companies/Index")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); 
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task JobOfferDetails_IdNotInDatabase_NotFoundResult()
        {
            var url = "/JobOffers/Details/0";
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
}
