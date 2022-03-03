using System.IO.Compression;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Xunit;
using System.Threading.Tasks;
using API.Entities;
using System.Collections.Generic;
using System.Linq;
using API.DbContexts;
using API.Repository;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WebApi.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DataSet dataset { get; private set; } = new();

        public DatabaseFixture()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database = HighScoreDb;";
            string queryString = "SELECT * FROM Highscores";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                var test = new SqlCommand(queryString, connection);
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                adapter.Fill(dataset);
            }
        }

        public void Dispose()
        {
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    public class highscoresControllerTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseFixture>
    {
        private readonly HttpClient _httpClient;
        private DatabaseFixture _databaseFixture;

        public highscoresControllerTests(WebApplicationFactory<Program> factory, DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            _httpClient = factory.CreateClient();
        }


        [Fact]
        public async Task GetHighScoresCountFromAPI_ShouldBeSameCountAsHighScoresInDatabase() //Endpoint = GetAllHighscoresAsync
        {
            //Arrange
            var expectedNumberOfHighscores = _databaseFixture.dataset.Tables[0].Rows.Count;

            //Act
            HttpResponseMessage? responseMessage = await _httpClient.GetAsync("/api/v01/highscores");
            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            var actualNumberOfHighscores = (await JsonSerializer.DeserializeAsync<List<Highscore>>(responseStream)).Count;

            //Assert
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.Equal(actualNumberOfHighscores, expectedNumberOfHighscores);
        }


        [Fact]
        public async Task PostHighScoreToAPI_HighScoresInDatabaseShouldIncreaseByOne() //Endpoint = AddHighscoreAsync
        {
            //Arrange
            var numberOfHighscoresBeforePost = _databaseFixture.dataset.Tables[0].Rows.Count;

            //Act
            string requestUri = "https://localhost:7288/api/v01/highscores";
            Highscore highscore = new Highscore() {Id = Guid.NewGuid(), Name = "JanneBananBannan", Time = 1666};
            var stringContent = new StringContent(JsonSerializer.Serialize(highscore), Encoding.UTF8, "application/json");
            var postResponseMessage = await _httpClient.PostAsync(requestUri, stringContent);
            
            HttpResponseMessage? getResponseMessage = await _httpClient.GetAsync("/api/v01/highscores");
            var responseStream = await getResponseMessage.Content.ReadAsStreamAsync();
            var numberOfHighscoresAfterPost = (await JsonSerializer.DeserializeAsync<List<Highscore>>(responseStream)).Count;

            //Assert
            Assert.Equal(HttpStatusCode.Created, postResponseMessage.StatusCode);
            Assert.Equal(numberOfHighscoresBeforePost+1, numberOfHighscoresAfterPost);
        }
    }
}