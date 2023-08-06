using System.Collections.Generic;
using System.Threading.Tasks;
using FutnorteApp.DataAccess;
using FutnorteApp.Domain;

namespace FutnorteApp.BusinessLogic
{
    internal class TeamService
    {
        private readonly TeamRepository _teamRepository;

        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        // Get all teams asynchronously.
        public async Task<List<Team>> GetAllTeams()
        {
            return await _teamRepository.GetAllTeams();
        }

        // Get a team by ID.
        public Team? GetTeamById(int teamId)
        {
            return _teamRepository.GetTeamById(teamId);
        }

        // Add a new team.
        public void AddTeam(Team team)
        {
            _teamRepository.AddTeam(team);
        }

        // Update an existing team.
        public void UpdateTeam(Team team)
        {
            _teamRepository.UpdateTeam(team);
        }

        // Delete a team by ID.
        public void DeleteTeam(int teamId)
        {
            _teamRepository.DeleteTeam(teamId);
        }
    }
}
