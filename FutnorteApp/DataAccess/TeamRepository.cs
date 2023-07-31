using System.Collections.Generic;
using System.Threading.Tasks;
using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FutnorteApp.DataAccess
{
    internal class TeamRepository
    {
        private readonly FutnorteContext _dbContext;

        public TeamRepository(FutnorteContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all teams asynchronously.
        public async Task<List<Team>> GetAllTeams()
        {
            return await _dbContext.Teams.ToListAsync();
        }

        // Get a team by ID.
        public Team? GetTeamById(int teamId)
        {
            return _dbContext.Teams.Find(teamId);
        }

        // Add a new team.
        public void AddTeam(Team team)
        {
            _dbContext.Teams.Add(team);
            _dbContext.SaveChanges();
        }

        // Update an existing team.
        public void UpdateTeam(Team team)
        {
            _dbContext.Teams.Update(team);
            _dbContext.SaveChanges();
        }

        // Delete a team by ID.
        public void DeleteTeam(int teamId)
        {
            var team = _dbContext.Teams.Find(teamId);
            if (team != null)
            {
                _dbContext.Teams.Remove(team);
                _dbContext.SaveChanges();
            }
        }
    }
}
