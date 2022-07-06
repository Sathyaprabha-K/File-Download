using LoginForm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using test.Models;

namespace test.Controllers
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
        [HttpPost]
        public IActionResult Index(Login login)
        {

            string myFileName = String.Format("{0}-{1}.txt", DateTime.Now.ToString("yyyyMMddhhmmss"), "Output");
            string path = Path.Combine(@"C:\Users\Public", myFileName);
            ViewBag.path = path;
            byte[] utf8bytesJson = JsonSerializer.SerializeToUtf8Bytes(login);
            string strResult = System.Text.Encoding.UTF8.GetString(utf8bytesJson);
            using (StreamWriter sw = System.IO.File.CreateText(path))
            {
                sw.WriteLine(strResult);
            }


            
           
           return View("View");
        }
        public ActionResult GetPdf(string fileName)
        {
            var fs = System.IO.File.OpenRead(fileName);
            return File(fs, "application/txt", fileName);
            
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
