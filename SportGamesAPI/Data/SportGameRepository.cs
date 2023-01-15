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
        public async Task<SportGame> CreateSportGame(string team1Name, string team2Name)
        {
            _context.SportGames.Add(new SportGame()
            {
                Team1Name = team1Name,
                Team2Name = team2Name,
                StartTime = DateTime.Now,
                Finished = false
            });
            await _context.SaveChangesAsync();
            return _context.SportGames.OrderByDescending(x => x.Id).FirstOrDefault();

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

        public async Task<SportGame> UpdateSportGame(int id, int team1Score, int team2Score)
        {
            var result = await _context.SportGames.FindAsync(id);
            if (result != null && result.Finished == false)
            {
                result.Team1Score = team1Score;
                result.Team2Score = team2Score;
                await _context.SaveChangesAsync();
                return result;
                
            }
            return null;
        }

        public async Task<SportGame> FinishSportGame(int id)
        {
            var result = await _context.SportGames.FindAsync(id);
            if (result != null)
            {
                result.Finished = true;
                result.EndTime = DateTime.Now;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
