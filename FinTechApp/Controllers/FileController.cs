namespace FinTechApp.Controllers;

using System;
using System.IO;
using System.Linq;
using FinTech_App.Dto;
using FinTech_App.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class FileController : Controller
{
    private readonly ILogger<FileController> _logger;
    private readonly FinTechDbContext _db;
    private readonly IWebHostEnvironment _appEnvironment;

    public FileController(ILogger<FileController> logger, FinTechDbContext db, IWebHostEnvironment appEnvironment)
    {
        _logger = logger;
        _db = db;
        _appEnvironment = appEnvironment;
    }


    public  IActionResult  Index()
    {

        FileModel? file = _db.Files.FirstOrDefault();
        if (file == null)         return View();
        string imgB64 = Convert.ToBase64String(file.FileData);
     

        var filePath = _appEnvironment.WebRootPath + "\\images\\" + file.FileName;

        System.IO.File.WriteAllBytes(filePath, file.FileData);

        FileModelDto fileDto = new FileModelDto
        {
            Base64 = imgB64,
            Name = "\\images\\" + file.FileName
        };
        return View(fileDto);
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            using MemoryStream ms = new();
            file.CopyTo(ms);
            byte[] fileBytes = ms.ToArray();
            var newFile = new FileModel
            {
                FileName = file.FileName,
                FileData = fileBytes
            };
            _db.Files.Add(newFile);
            _db.SaveChanges();
        }

        return RedirectToAction("Index", "Home"); // Redirect to home or wherever you want
    }
}
