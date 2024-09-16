using GameZone.Models;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //but Register as User
        [HttpPost]
        public async Task<IActionResult> Register(UserVM userVM)
        {
            if (ModelState.IsValid)
            {

                User user = new User()
                {
                    UserName = userVM.UserName,
                    PasswordHash = userVM.Password,
                    address = userVM.Address
                };
                IdentityResult result = await userManager.CreateAsync(user,userVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,"User");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        // Map specific error to the respective field if possible
                        if (error.Code.Contains("Password"))
                        {
                            ModelState.AddModelError("Password", error.Description);
                        }
                        else if (error.Code.Contains("UserName"))
                        {
                            ModelState.AddModelError("UserName", error.Description);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                }
            }
            return View(userVM);
        }


        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> RegisterAdmin(UserVM userVM)
        {
            if (ModelState.IsValid)
            {

                User user = new User()
                {
                    UserName = userVM.UserName,
                    PasswordHash = userVM.Password,
                    address = userVM.Address
                };
                IdentityResult result = await userManager.CreateAsync(user, userVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        // Map specific error to the respective field if possible
                        if (error.Code.Contains("Password"))
                        {
                            ModelState.AddModelError("Password", error.Description);
                        }
                        else if (error.Code.Contains("UserName"))
                        {
                            ModelState.AddModelError("UserName", error.Description);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                }
            }
            return View(userVM);
        }



        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UserLoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                // Get the user by username
                User? user = await userManager.FindByNameAsync(userVM.UserName);
                if (user != null)
                {
                    // Check if the password is correct
                    bool isCorrect = await userManager.CheckPasswordAsync(user, userVM.Password);
                    if (isCorrect)
                    {
                        // Sign in the user
                        await signInManager.SignInAsync(user, isPersistent: userVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Add model error if the password is wrong
                        ModelState.AddModelError("Password", "Incorrect password.");
                    }
                }
                else
                {
                    // Add model error if the user is not found
                    ModelState.AddModelError("UserName", "User not found.");
                }
            }
            // Add a generic error message if model state is invalid
            return View(userVM);
        }

        public async Task<IActionResult> LogOut()
        {
           await signInManager.SignOutAsync();
           return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult>AddRole()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddRole(RoleVM roleVM)
        {
            if (ModelState.IsValid) { 
                IdentityRole identityRole = new IdentityRole()
                {
                    Name=roleVM.Role
                };
                IdentityResult Result = await roleManager.CreateAsync(identityRole);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach(var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(roleVM);
        }
    }
}
