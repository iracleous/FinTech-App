using FinTech_App.Model;
using FinTechApp.Communication;
using FinTechApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Policy;

namespace FinTechApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientDataService _dataService;
        private readonly FinTechDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, 
            IClientDataService clientDataService,  FinTechDbContext db, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _dataService = clientDataService;
            _db = db;
            _appEnvironment = appEnvironment;
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
            return RedirectToAction("Client");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(long clientId)
        {
            await _dataService.DeleteClientAsync(clientId);
            return RedirectToAction("Client");
        }

        public IActionResult ImageUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoUpload(List<IFormFile> files, string description)
        {
            long size = files.Sum(file => file.Length);
            foreach(var file in files)
            {
                //   var filePath = Path.GetTempFileName();

                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var imageDirectory = MyConfig.GetValue<string>("ImageDirectory");
                if (imageDirectory == null) imageDirectory = "c:\\images";

                imageDirectory = _appEnvironment.WebRootPath;

                var filePath = imageDirectory + "\\images\\" + file.FileName;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                _db.Images.Add(new ImageTemplate
                {
                     CreatedDate = DateTime.Now,
                     Description = description,
                     FileName = file.FileName,
                     Path= imageDirectory
                });
                await _db.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Images()
        {
            List<ImageTemplate> images = await _db.Images.ToListAsync();
            return View(images);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
