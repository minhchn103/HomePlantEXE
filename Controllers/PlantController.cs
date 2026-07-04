using Google.Cloud.Firestore;
using HomePlant.Models;
using HomePlant.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomePlant.Controllers;

public class PlantController : Controller
{
    private readonly PlantService _plantService;
    private readonly FirestoreDb _db;

    public PlantController(
        PlantService plantService,
        FirestoreDb db)
    {
        _plantService = plantService;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var plants =
            await _plantService.GetAllPlants();

        return View(plants);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        PlantModel plant)
    {
        plant.CreatedAt =
            Timestamp.GetCurrentTimestamp();

        await _plantService.AddPlant(plant);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(
        string id)
    {
        var doc = await _db
            .Collection("plants")
            .Document(id)
            .GetSnapshotAsync();

        if (!doc.Exists)
            return NotFound();

        var plant =
            doc.ConvertTo<PlantModel>();

        return View(plant);
    }

    public async Task<IActionResult> Delete(
        string id)
    {
        await _plantService.DeletePlant(id);

        return RedirectToAction(nameof(Index));
    }
}