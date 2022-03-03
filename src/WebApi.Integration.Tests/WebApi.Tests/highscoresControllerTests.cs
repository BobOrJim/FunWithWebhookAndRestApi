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

namespace WebApi.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public SqlConnection Db { get; private set; }

        public DatabaseFixture()
        {
            Db = new SqlConnection("Server=(localdb)\\MSSQLLocalDB; Database = HighScoreDb;");
            // ... initialize data in the test database ...
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
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
        private readonly HighScoreRepository _iHighScoreRepository;
        private DatabaseFixture _fixture;

        public highscoresControllerTests(WebApplicationFactory<Program> factory, DatabaseFixture fixture)
        {
            _fixture = fixture;
            //https://xunit.net/docs/shared-context
            _httpClient = factory.CreateClient();
        }


        [Fact]
        public async Task GetAllHighscoresAsync_Tests()
        {
            var conn = _fixture.Db;
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Highscore", conn);

            //command.ExecuteNonQuery();

            Console.WriteLine("asdf");
            


            //int result = command.ExecuteNonQuery();
            //using (SqlDataReader reader = command.ExecuteReader())
            //{
            //    if (reader.Read())
            //    {
            //        Console.WriteLine("Hej");
            //    }
            //}



            conn.Close();

            //Arrange
            //var expectedNumberOfHighscores = (await _iHighScoreRepository.GetAllHighscoresAsync()).Count();
            var expectedNumberOfHighscores = 21;

            //Act
            HttpResponseMessage? responseMessage = await _httpClient.GetAsync("/api/v01/highscores");
            //responseMessage.EnsureSuccessStatusCode();
            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            var actualNumberOfHighscores = (await JsonSerializer.DeserializeAsync<List<Highscore>>(responseStream)).Count; //, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

            //Assert
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.Equal(actualNumberOfHighscores, expectedNumberOfHighscores);
        }



        //[HttpGet]
            //[Route("", Name = "GetAllHighscoresAsync")]
            //[HttpPost]
            //[Route("", Name = "AddHighscoreAsync")]
            //[HttpGet]
            //[Route("{id:Guid}", Name = "GetHighscoreByIdAsync")]
            //[HttpGet]
            //[Route("{name}", Name = "GetHighscoreByNameAsync")]
            //[HttpDelete]
            //[Route("", Name = "DeleteAllHighscores")]

        }
}