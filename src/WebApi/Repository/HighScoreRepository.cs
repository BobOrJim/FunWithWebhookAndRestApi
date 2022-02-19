using API.DbContexts;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{

    public class HighScoreRepository : IHighScoreRepository
    {
        private readonly HighScoreDbContext _highScoreDbContext;

        public HighScoreRepository(HighScoreDbContext highScoreDbContext) => _highScoreDbContext = highScoreDbContext;

        public async Task<IEnumerable<Highscore>> GetAllHighscoresAsync()
        {
            try
            {
                return await _highScoreDbContext.Highscores.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Highscore>();
            }
        }

        public async Task<bool> AddHighscoreAsync(Highscore highscore)
        {
            if (highscore == null)
            {
                throw new ArgumentNullException(nameof(highscore));
            }
            try
            {
                _highScoreDbContext.Highscores.Add(highscore);
                return (await _highScoreDbContext.SaveChangesAsync() > 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw; //I want to bubble this error to the controller, in order to be able to send a InternalServerError with message "Duplicate Id's not allowed"
            }
        }

        public async Task<Highscore?> GetHighscoreByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            try
            {
                return await _highScoreDbContext.Highscores.FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Highscore>> GetHighscoresByNameAsync(string name)
        {
            if (name == string.Empty)
            {
                throw new ArgumentNullException(nameof(name));
            }
            try
            {
                return await _highScoreDbContext.Highscores.Where(h => h.Name == name).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Highscore>();
            }
        }

        public async Task<int> DeleteAllHighscoresAsync() => await _highScoreDbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Highscores");

    }
}