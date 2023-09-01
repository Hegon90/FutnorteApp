using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutnorteApp.DataAccess
{
    internal class ResultRepository
    {
        private readonly FutnorteContext _dbContext;

        public ResultRepository(FutnorteContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all the results Asynchronously
        public async Task<List<Result>> GetAllResults()
        {
            return await _dbContext.Results.ToListAsync();
        }

        // Get result by Id.
        public Result? GetResultById(int resultId)
        {
            return _dbContext.Results
                .Include(m => m.Match)
                .FirstOrDefault(m => m.ResultId == resultId);
        }

        // Add new result
        public void AddResult(Result result)
        {
            _dbContext.Results.Add(result);
            _dbContext.SaveChanges();
        }

        // Update existing result
        public void UpdateResult(Result result)
        {
            _dbContext.Results.Update(result);
            _dbContext.SaveChanges();
        }

        // Delete result by Id.
        public void DeleteResult(int resultId)
        {
            var result = _dbContext.Results.Find(resultId);
            if (result != null)
            {
                _dbContext.Results.Remove(result);
                _dbContext.SaveChanges();
            }
        }
    }
}
