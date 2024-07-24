using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales_NET8.Web.Data;
using Sales_NET8.Web.Data.Entities;
using Sales_NET8.Web.Models;
using System.Diagnostics;
using System.Linq;

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
            // Receber a lista de países com o repositório
            var countries = _repository.GetCountries().ToList();
            return View(countries);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

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
            // Exemplo: enviar a mensagem por email
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
