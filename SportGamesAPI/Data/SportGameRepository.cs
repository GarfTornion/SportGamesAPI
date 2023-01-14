using SportGamesAPI.Data.Interfaces;

namespace SportGamesAPI.Data
{
    public class SportGameRepository : ISportGameRepository
    {
        private readonly SportGameDbContext _context;

        public SportGameRepository(SportGameDbContext context)
        {
            _context = context;
        }
        public async Task<SportGame> CreateSportGame(SportGame sportGame)
        {
            _context.SportGames.Add(sportGame);
            await _context.SaveChangesAsync();

            return sportGame;
        }
        
        public async Task<SportGame> GetSportGame(int id)
        {
            var result = await _context.SportGames.FindAsync(id);
            return result;

        }
        
        public async Task<List<SportGame>> GetSportGames()
        {
            var result = await _context.SportGames.ToListAsync();
            return result;

        }

        public async Task<SportGame> UpdateSportGame(SportGame sportGame)
        {
            _context.SportGames.Update(sportGame);
            await _context.SaveChangesAsync();
            return sportGame;
        }
    }
}
