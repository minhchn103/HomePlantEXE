using HomePlant.Models;
using HomePlant.Services;
using HomePlant.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomePlant.Controllers;

public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(
        UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        RegisterVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = new UserModel
        {
            FullName = vm.FullName,
            Email = vm.Email,
            Phone = vm.Phone,
            Role = "User",
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        await _userService.Create(user);

        HttpContext.Session.SetString(
            "Email",
            user.Email);

        HttpContext.Session.SetString(
            "Role",
            user.Role);

        return RedirectToAction(
            "Index",
            "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Login(
        LoginVM vm)
    {
        var user =
            await _userService
            .GetByEmail(vm.Email);

        if (user == null)
        {
            ModelState.AddModelError(
                "",
                "Sai tài khoản");

            return View(vm);
        }

        HttpContext.Session.SetString(
            "Email",
            user.Email);

        HttpContext.Session.SetString(
            "Role",
            user.Role);

        return RedirectToAction(
            "Index",
            "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction(
            "Index",
            "Home");
    }
}