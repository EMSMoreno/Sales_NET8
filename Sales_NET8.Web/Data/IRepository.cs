﻿using Sales_NET8.Web.Data.Entities;

namespace Sales_NET8.Web.Data
{
    public interface IRepository
    {
        // Countries
        void AddCountry(Country country);
        bool CountryExists(int id);
        bool CountryNameExists(string name);
        IEnumerable<Country> GetCountries();
        Country GetCountry(int id);
        void RemoveCountry(Country country);
        Task<bool> SaveAllAsync();
        void UpdateCountry(Country country);

        // Categories
        void AddCategory(Category category);
        bool CategoryExists(int id);
        bool CategoryNameExists(string name);
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void RemoveCategory(Category category);
        void UpdateCategory(Category category);

    }
}