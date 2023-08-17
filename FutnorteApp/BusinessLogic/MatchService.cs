using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutnorteApp.BusinessLogic
{
    internal class MatchService
    {
        private readonly MatchRepository _matchRepository;
        private readonly RoundRepository _roundRepository;
        private readonly TeamRepository _teamRepository;
        private readonly PlaceRepository _placeRepository;
        public MatchService(MatchRepository matchRepository, RoundRepository roundRepository, 
            TeamRepository teamRepository, PlaceRepository placeRepository)
        {
            _matchRepository = matchRepository;
            _roundRepository = roundRepository;
            _teamRepository = teamRepository;
            _placeRepository = placeRepository;
        }

        // Get all matches asynchronously
        public async Task<List<Match>> GetAllMatches()
        {
            return await _matchRepository.GetAllMatches();
        }

        // Get match by Id
        public Match? GetMatchById(int matchId)
        {
            return _matchRepository.GetMatchById(matchId);
        }

        // Add new match
        public void AddMatch(Match match)
        {
            _matchRepository.AddMatch(match);
        }

        // Update existing match
        public void UpdateMatch(Match match)
        {
            _matchRepository.UpdateMatch(match);
        }

        // Delete match by Id
        public void DeleteMatch(int matchId)
        {
            _matchRepository.DeleteMatch(matchId);
        }

        // Get all rounds Asynchronously
        public async Task<List<Round>> GetAllRounds()
        {
            return await _roundRepository.GetAllRounds();
        }

        // Get all teams Asynchronously
        public async Task<List<Team>> GetAllTeams()
        {
            return await _teamRepository.GetAllTeams();
        }

        // Get all places Asynchronously   
        public async Task<List<Place>> GetAllPlaces()
        {
            return await _placeRepository.GetAllPlaces();
        }
    }
}
