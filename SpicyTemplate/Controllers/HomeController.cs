using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyTemplate.DAL;
using SpicyTemplate.Models;
using SpicyTemplate.ViewModels;
using System.Diagnostics;

namespace SpicyTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<OurMenu> menu = await _context.Menus.Include(m=>m.Category).ToListAsync();
            HomeVM homeVM= new HomeVM
            { 
                OurMenus = menu
            };
            return View(homeVM);
        }

   
    }
}
