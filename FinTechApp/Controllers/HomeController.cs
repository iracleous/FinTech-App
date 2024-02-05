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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Client()
        {
            return View(ClientData.GetClients());
        }

        public async Task<IActionResult> aClient(int id)
        {
            return View(await ClientData.GetClientAsync(id));
        }

        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoCreateClient(Client client) {
            await ClientData.CreateClientAsync(client);
            return Redirect("Client");
        }

        [HttpPost]
        public IActionResult DeleteClient(long clientId)
        {
            ClientData.DeleteClient(clientId);
            return Redirect("Client");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
