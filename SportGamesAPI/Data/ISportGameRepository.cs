namespace SportGamesAPI.Data.Interfaces
{
    public interface ISportGameRepository
    {
        Task<List<SportGame>> GetSportGames();

        Task<SportGame> GetSportGame(int id);

        Task<SportGame> CreateSportGame(string team1Name, string team2Name);

        Task<SportGame> UpdateSportGame(int id, int team1Score, int team2Score);

        Task<SportGame> FinishSportGame(int id);
    }
}
