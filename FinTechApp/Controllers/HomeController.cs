using FinTech_App.Model;
using FinTechApp.Communication;
using FinTechApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FinTechApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientDataService _dataService;

        public HomeController(ILogger<HomeController> logger, IClientDataService clientDataService)
        {
            _logger = logger;
            _dataService = clientDataService;
        }

        public IActionResult Index()
        {
            _logger.Log(LogLevel.Information, "Index was requested");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Client()
        {
            return View(await _dataService.GetClientsAsync());
        }

        public async Task<IActionResult> aClient(int id)
        {
            return View(await _dataService.GetClientAsync(id));
        }

        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoCreateClient(Client client) {
            await _dataService.CreateClientAsync(client);
            return Redirect("Client");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(long clientId)
        {
            await _dataService.DeleteClientAsync(clientId);
            return Redirect("Client");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
