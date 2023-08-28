using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutnorteApp.DataAccess
{
    internal class MatchRepository
    {
        private readonly FutnorteContext _dbContext;

        public MatchRepository(FutnorteContext dbContext)
        {
            _dbContext = dbContext;
        }   

        // Get all the matches Asynchronously
        public async Task<List<Match>> GetAllMatches()
        {
            return await _dbContext.Matches.ToListAsync();
        }

        // Get match by Id
        //public Match? GetMatchById(int matchId) 
        //{
        //    return _dbContext.Matches.Find(matchId);
        //}
        public Match? GetMatchById(int matchId)
        {
            return _dbContext.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Round)
                .Include(m => m.Field)
                .FirstOrDefault(m => m.MatchId == matchId);
        }

        // Add new match
        public void AddMatch(Match match)
        {
            _dbContext.Matches.Add(match);
            _dbContext.SaveChanges();
        }

        // Update existing match
        public void UpdateMatch(Match match)
        {
            _dbContext.Matches.Update(match);
            _dbContext.SaveChanges();
        }


        // Delete match
        public void DeleteMatch(int matchId)
        {
            var match = _dbContext.Matches.Find(matchId);
            if (match != null)
            {
                _dbContext.Matches.Remove(match);
                _dbContext.SaveChanges();
            }
        }
    }
}
