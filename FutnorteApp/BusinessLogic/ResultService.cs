using FutnorteApp.DataAccess;
using FutnorteApp.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutnorteApp.BusinessLogic
{
    internal class ResultService
    {
        private readonly ResultRepository _resultRepository;
        private readonly MatchRepository _matchRepository;

        public ResultService(ResultRepository resultRepository, MatchRepository matchRepository)
        {
            _resultRepository = resultRepository;
            _matchRepository = matchRepository;
        }

        // Get all results asynchronously.
        public async Task<List<Result>> GetAllResults()
        {
            return await _resultRepository.GetAllResults();
        }

        // Get result by Id.
        public Result? GetResultById(int resultId)
        {
            return _resultRepository.GetResultById(resultId);
        }

        // Add new result
        public void AddResult(Result result)
        {
            _resultRepository.AddResult(result);
        }

        // Update existing result
        public void UpdateResult(Result result)
        {
            _resultRepository.UpdateResult(result);
        }

        // Delete result by Id.
        public void DeleteResult(int resultId)
        {
            _resultRepository.DeleteResult(resultId);
        }

        // Get all matches asynchronously.
        public async Task<List<Match>> GetAllMatches()
        {
            return await _matchRepository.GetAllMatches();
        }
    }
}
