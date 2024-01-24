using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpicyTemplate.Areas.Manage.ViewModels;
using SpicyTemplate.DAL;
using SpicyTemplate.Models;
using SpicyTemplate.Utulities.Extentions;

namespace SpicyTemplate.Areas.Manage.Controllers
{
    [Area("manage")]
    public class OurMenuController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public OurMenuController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task< IActionResult> Index()
        {
            List<OurMenu> ourMenus=await _context.Menus.ToListAsync();
            return View(ourMenus);
        }
        public async Task<IActionResult> Create()
        {
            List<Category> categories= await _context.Categories.ToListAsync();
            if (categories == null) return NotFound();
            CreateVM vm = new CreateVM
            {
                categories = categories
            };
        
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createVM)
        {
            createVM.categories = await _context.Categories.ToListAsync();
            if (ModelState.IsValid)
            {
                return View(createVM);

            }

            bool result = await _context.Menus.AnyAsync(c=>c.Name==createVM.Name);
            if (result)
            {
                ModelState.AddModelError("Name", "errror");
                return View(createVM);
            }
            if (!createVM.Image.CheckSize(2*1024))
            {
                ModelState.AddModelError("Image", "selil sefdir");
                return View(createVM);
            }

            if (!createVM.Image.CheckType("image/"))
            {
                ModelState.AddModelError("Image", "Type sefdir");
                return View(createVM);
            }
            string filname = await createVM.Image.CreafeFileAsync(_environment.WebRootPath, "assets", "uploads");

            OurMenu ourMenu = new OurMenu
            {
                Img = filname,
                Name = createVM.Name,
                Price = createVM.Price
            };
            await _context.Menus.AddAsync(ourMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");

        }
        public IActionResult Update()
        {
            return View();
        }
    }
}
