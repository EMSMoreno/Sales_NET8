using Microsoft.EntityFrameworkCore;
using Sales_NET8.Web.Data.Entities;
using System.Threading.Tasks;

namespace Sales_NET8.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await SeedCountriesAsync();
            await SeedCategoriesAsync();
        }

        private async Task SeedCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                AddCountry("Argentina");
                AddCountry("Colombia");
                AddCountry("Peru");
                AddCountry("Venezuela");

                await _context.SaveChangesAsync();
            }
        }

        private void AddCountry(string name)
        {
            _context.Countries.Add(new Country
            {
                Name = name
            });
        }

        private async Task SeedCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                AddCategory("Categoria A");
                AddCategory("Categoria B");
                AddCategory("Categoria C");
                AddCategory("Categoria D");
                AddCategory("Categoria E");

                await _context.SaveChangesAsync();
            }
        }

        private void AddCategory(string name)
        {
            _context.Categories.Add(new Category
            {
                Name = name
            });
        }
    }
}