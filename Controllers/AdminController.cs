using Google.Cloud.Firestore;
using HomePlant.Services;
using HomePlant.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomePlant.Controllers;

public class AdminController : Controller
{
    private readonly FirestoreDb _db;

    private readonly UserService _userService;

    private readonly PlantSampleService _plantService;

    public AdminController(
        FirestoreDb db,
        UserService userService,
        PlantSampleService plantService)
    {
        _db = db;
        _userService = userService;
        _plantService = plantService;
    }

    public async Task<IActionResult> Dashboard()
    {
        var users =
            await _db.Collection("users")
                .GetSnapshotAsync();

        var plants =
            await _db.Collection("plants")
                .GetSnapshotAsync();

        var articles =
            await _db.Collection("articles")
                .GetSnapshotAsync();

        var diagnoses =
            await _db.Collection("diagnoses")
                .GetSnapshotAsync();

        var vm = new AdminDashboardVM
        {
            UserCount = users.Count,
            PlantCount = plants.Count,
            ArticleCount = articles.Count,
            DiagnosisCount = diagnoses.Count
        };

        return View(vm);
    }

    public async Task<IActionResult> Users()
    {
        var users =
            await _userService.GetAll();

        return View(users);
    }

    public async Task<IActionResult> Ban(
        string id)
    {
        await _userService.BanUser(id);

        return RedirectToAction(nameof(Users));
    }

    public async Task<IActionResult> UnBan(
        string id)
    {
        await _userService.UnBanUser(id);

        return RedirectToAction(nameof(Users));
    }
}