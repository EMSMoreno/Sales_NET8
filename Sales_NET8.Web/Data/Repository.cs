using Microsoft.EntityFrameworkCore;
using Sales_NET8.Web.Data.Entities;

namespace Sales_NET8.Web.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries.OrderBy(c => c.Name);
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Find(id);
        }

        public void AddCountry(Country country)
        {
            _context.Countries.Add(country);
        }

        public void UpdateCountry(Country country)
        {
            _context.Countries.Update(country);
        }

        public void RemoveCountry(Country country)
        {
            _context.Countries.Remove(country);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        public bool CountryNameExists(string name)
        {
            return _context.Countries.Any(e => e.Name == name);
        }
    }
}
