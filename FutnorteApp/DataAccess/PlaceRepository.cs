using System.Collections.Generic;
using System.Threading.Tasks;
using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FutnorteApp.DataAccess
{
    internal class PlaceRepository
    {
        private readonly FutnorteContext _dbContext;

        public PlaceRepository(FutnorteContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all places asynchronously
        public async Task<List<Place>> GetAllPlaces()
        {
            return await _dbContext.Places.ToListAsync();
        }

        // Get place by Id
        public Place? GetPlaceById(int placeId)
        {
            return _dbContext.Places.Find(placeId);
        }

        // Add new place
        public void AddPlace(Place place)
        {
            _dbContext.Places.Add(place);
            _dbContext.SaveChanges();
        }

        // Update existing place
        public void UpdatePlace(Place place)
        {
            _dbContext.Places.Update(place);
            _dbContext.SaveChanges();
        }

        // Delete place by id
        public void DeletePlace(int placeId)
        {
            var place = _dbContext.Places.Find(placeId);
            if (place != null)
            {
                _dbContext.Places.Remove(place);
                _dbContext.SaveChanges();
            }
        }
    }
}
