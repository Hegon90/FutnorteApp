using System.Collections.Generic;
using System.Threading.Tasks;
using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace FutnorteApp.DataAccess
{
    internal class FieldRepository
    {
        private readonly FutnorteContext _dbContext;

        public FieldRepository(FutnorteContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all fields asynchronously
        public async Task<List<Field>> GetAllFields()
        {
            return await _dbContext.Fields.ToListAsync();
        }

        // Get field by Id
        public Field? GetFieldById(int fieldId)
        {
            return _dbContext.Fields.Find(fieldId);
        }

        // Add new field
        public void AddField(Field field)
        {
            _dbContext.Fields.Add(field);
            _dbContext.SaveChanges();
        }

        // Update existing field
        public void UpdatePlace(Field field)
        {
            _dbContext.Fields.Update(field);
            _dbContext.SaveChanges();
        }

        // Delete field by Id
        public void DeleteField(int fieldId)
        {
            var field = _dbContext.Fields.Find(fieldId);
            if (field != null)
            {
                _dbContext.Fields.Remove(field);
                _dbContext.SaveChanges();
            }
        }
    }
}
