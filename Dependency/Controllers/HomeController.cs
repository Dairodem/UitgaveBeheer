using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dependency.Models;
using Dependency.Services;

namespace Dependency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService _scopedService;
        private readonly ISingletonService _singletonService;
        private readonly ITranscientService _transcientService;

        public HomeController(
            ILogger<HomeController> logger, 
            IScopedService scopedService, 
            ISingletonService singletonService, 
            ITranscientService transcientService)
        {
            
            _logger = logger;
            _scopedService = scopedService;
            _singletonService = singletonService;
            _transcientService = transcientService;
        }

        public IActionResult Index()
        {
            var scoped = _scopedService.ToonGuid();
            var single = _singletonService.ToonGuid();
            var transient = _transcientService.ToonGuid();
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
