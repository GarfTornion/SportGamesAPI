namespace SportGamesAPI.Data.Interfaces
{
    public interface ISportGameRepository
    {
        Task<List<SportGame>> GetSportGames();

        Task<SportGame> GetSportGame(int id);

        Task<SportGame> CreateSportGame(SportGame sportGame);

        Task<SportGame> UpdateSportGame(SportGame sportGame);
    }
}
