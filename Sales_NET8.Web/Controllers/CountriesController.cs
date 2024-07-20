using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales_NET8.Web.Data;
using Sales_NET8.Web.Data.Entities;

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
            return View(_repository.GetCountries());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar as alterações. Por favor, tente novamente mais tarde. Se o problema persistir, entre em contato com o suporte.");
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
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

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = _repository.GetCountry(id);
            _repository.RemoveCountry(country);
            await _repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
