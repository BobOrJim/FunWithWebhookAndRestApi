using API.Entities;

namespace API.Repository
{
    public interface IHighScoreRepository
    {
        Task<IEnumerable<Highscore>> GetAllHighscoresAsync();
        Task<bool> AddHighscoreAsync(Highscore highscore);
        Task<Highscore?> GetHighscoreByIdAsync(Guid id);
        Task<IEnumerable<Highscore>> GetHighscoresByNameAsync(string name);
        Task<int> DeleteAllHighscoresAsync();
    }
}
