using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpicyTemplate.Areas.Manage.ViewModels;
using SpicyTemplate.Models;

namespace SpicyTemplate.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _useman;
		private readonly SignInManager<AppUser> _signman;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<AppUser> useman,SignInManager<AppUser> signmanager,RoleManager<IdentityRole>roleManager)
        {
			_useman = useman;
			_signman = signmanager;
			_roleManager = roleManager;
		}
        public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM vm)
		{
			if (!ModelState.IsValid) return View(vm);
			AppUser user = new AppUser
			{
				Name = vm.Name,  
				Surname = vm.Surname,
				Email = vm.Email,
				UserName = vm.Username
			};
			var result=await _useman.CreateAsync(user,vm.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(string.Empty, item.Description);
					
				}
				return View(vm);	
			}
			await _signman.SignInAsync(user,false);

			return RedirectToAction("Index","Home");
			


		}
		public IActionResult Login()
		{
			return View();	
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM vm)
		{
			if (!ModelState.IsValid) return View(vm);
			AppUser user = await _useman.FindByEmailAsync(vm.UsernameOrEmail);
			if (user == null)
			{
				user=await _useman.FindByNameAsync(vm.UsernameOrEmail);
				if(user == null)
				{
					ModelState.AddModelError(string.Empty, "eror");
					return View(vm);



				}

			}
		       var result=await  _signman.PasswordSignInAsync(user,vm.Password,vm.isRemembered,true);
			if (!result.Succeeded)
			{
				ModelState.AddModelError(string.Empty, "error");
				return View(vm);
			}
			if(result.IsLockedOut)
			{
				ModelState.AddModelError(string.Empty, "error");
				return View(vm);
			}
			return RedirectToAction("Index", "Home");
		}
		public async Task<IActionResult> LogOut()
		{
			await _signman.SignOutAsync();
			return RedirectToAction("Index","Home");
		}




	}
}
