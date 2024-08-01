using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales_NET8.Web.Data;
using Sales_NET8.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Sales_NET8.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IRepository _repository;

        public CountriesController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Countries
        public IActionResult Index()
        {
            var countries = _repository.GetCountries();
            return View(countries);
        }

        // GET: Countries/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _repository.GetCountry(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                if (_repository.GetCountries().Any(c => c.Name == country.Name))
                {
                    ModelState.AddModelError("Name", "O nome do país já existe. Por favor, insira um nome diferente.");
                    return View(country);
                }

                try
                {
                    _repository.AddCountry(country);
                    await _repository.SaveAllAsync();
                    TempData["SuccessMessage"] = "País inserido com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar as alterações. Por favor, tente novamente mais tarde.");
                }
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _repository.GetCountry(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateCountry(country);
                    await _repository.SaveAllAsync();
                    TempData["SuccessMessage"] = "País atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.CountryExists(country.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _repository.GetCountry(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = _repository.GetCountry(id);
            if (country == null)
            {
                return NotFound();
            }

            _repository.RemoveCountry(country);
            await _repository.SaveAllAsync();
            TempData["SuccessMessage"] = "País removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}