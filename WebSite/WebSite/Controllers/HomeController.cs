using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment _oHostingEnvironment;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(IWebHostEnvironment oHostingEnvironment)
        {
            _oHostingEnvironment = oHostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Others()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult UploadFiles() 
        {
            long size = 0;
            var files = Request.Form.Files;

            foreach (var file in files)
            {
                string fileName = "";
                fileName = _oHostingEnvironment.WebRootPath + $@"\Files\{file.FileName}";

                size += file.Length;
                try
                {
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                catch (Exception)
                {
                    break;
                }

            }  

            string message = $"{files.Count} files and {size} bytes upload.";
            return Json(message);
        }
    }
}
