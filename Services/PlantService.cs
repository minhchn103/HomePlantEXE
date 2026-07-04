using Google.Cloud.Firestore;
using HomePlant.Models;

namespace HomePlant.Services;

public class PlantService
{
    private readonly FirestoreDb _db;

    public PlantService(FirestoreService firestore)
    {
        _db = firestore.Db;
    }

    public async Task<List<PlantModel>> GetAllPlants()
    {
        QuerySnapshot snapshot =
            await _db.Collection("plants")
                     .GetSnapshotAsync();

        return snapshot.Documents
            .Select(x => x.ConvertTo<PlantModel>())
            .ToList();
    }

    public async Task AddPlant(PlantModel plant)
    {
        await _db.Collection("plants")
            .AddAsync(plant);
    }

    public async Task<PlantModel?> GetById(string id)
    {
        var doc = await _db
            .Collection("plants")
            .Document(id)
            .GetSnapshotAsync();

        if (!doc.Exists)
            return null;

        return doc.ConvertTo<PlantModel>();
    }

    public async Task DeletePlant(string id)
    {
        await _db.Collection("plants")
            .Document(id)
            .DeleteAsync();
    }
}