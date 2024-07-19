using Sales_NET8.Web.Data.Entities;
using System;

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
    }
}
