using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prose.Data;
using Prose.Models;

namespace Prose.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<IActionResult> Index()
        {
            var nytBooks = await NYTBestSellerAsync();
            return View(nytBooks);
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

        private static async Task<NYTBooksAPI> NYTBestSellerAsync()
        {
            string key = "Hdo7MddPNfc8wclWMh6kY5cR9e1A46Ex";

            string date = "current";

            string list = "Combined Print and E-Book Fiction";

            try
            {
                using (var client = new HttpClient())
                {
                    var content = await client.GetStringAsync($"https://api.nytimes.com/svc/books/v3/lists/{date}/{list}.json?api-key={key}");
                    return JsonConvert.DeserializeObject<NYTBooksAPI>(content);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
