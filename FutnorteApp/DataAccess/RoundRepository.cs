using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutnorteApp.DataAccess
{
    internal class RoundRepository
    {
        private readonly FutnorteContext _dbContext;

        public RoundRepository(FutnorteContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all rounds asynchronously
        public async Task<List<Round>> GetAllRounds()
        {
            return await _dbContext.Rounds.ToListAsync();
        }

        // Get a round by Id
        public Round? GetRoundById(int roundId)
        {
            return _dbContext.Rounds.Find(roundId);
        }

        // Add new round
        public void AddRound(Round round)
        {
            _dbContext.Rounds.Add(round);
            _dbContext.SaveChanges();
        }

        // Update existing round
        public void UpdateRound(Round round)
        {
            _dbContext.Rounds.Update(round);
            _dbContext.SaveChanges();
        }

        // Delete round by Id
        public void DeleteRound(int roundId)
        {
            var round = _dbContext.Rounds.Find(roundId);
            if (round != null)
            {
                _dbContext.Rounds.Remove(round);
                _dbContext.SaveChanges();
            }
        }
    }
}
