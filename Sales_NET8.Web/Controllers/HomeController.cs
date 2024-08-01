using Microsoft.AspNetCore.Mvc;
using Sales_NET8.Web.Data;
using Sales_NET8.Web.Data.Entities;
using Sales_NET8.Web.Models;
using System.Diagnostics;

namespace Sales_NET8.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;

        public HomeController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            // Receber a lista de pa�ses com o reposit�rio
            var countries = _repository.GetCountries().ToList();
            return View(countries);
        }

        // M�todos para criar um novo pa�s
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _repository.AddCountry(country);
                await _repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // M�todos para editar um pa�s existente
        public IActionResult Edit(int id)
        {
            var country = _repository.GetCountry(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

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
                _repository.UpdateCountry(country);
                await _repository.SaveAllAsync();
                TempData["SuccessMessage"] = "Pa�s atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // M�todos para apagar/remover um pa�s
        public IActionResult Delete(int id)
        {
            var country = _repository.GetCountry(id);
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
            _repository.RemoveCountry(country);
            await _repository.SaveAllAsync();
            TempData["SuccessMessage"] = "Pa�s removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string Name, string Email, string Message)
        {
            // Exemplo: enviar uma mensagem por email
            // EmailService.Send(Email, Message);

            ViewBag.Message = "A tua mensagem foi enviada com sucesso!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}