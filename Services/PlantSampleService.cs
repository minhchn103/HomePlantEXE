using Google.Cloud.Firestore;
using HomePlant.Models;
using System.Collections.Generic;

namespace HomePlant.Services;

public class PlantSampleService
{
    private readonly FirestoreDb _db;

    public PlantSampleService(FirestoreService firestore)
    {
        _db = firestore.Db;
    }

    public async Task<List<PlantSampleModel>> GetAll()
    {
        var snapshot = await _db
            .Collection("plantSamples")
            .GetSnapshotAsync();

        return snapshot.Documents
            .Select(x => x.ConvertTo<PlantSampleModel>())
            .ToList();
    }

    public async Task Add(PlantSampleModel plant)
    {
        await _db
            .Collection("plantSamples")
            .AddAsync(plant);
    }

    public async Task Delete(string id)
    {
        await _db
            .Collection("plantSamples")
            .Document(id)
            .DeleteAsync();
    }

    public async Task<List<PlantSampleModel>> GetTopPlants(int count = 4)
    {
        var snapshot = await _db
            .Collection("plantSamples")
            .Limit(count)
            .GetSnapshotAsync();

        return snapshot.Documents
            .Select(x => x.ConvertTo<PlantSampleModel>())
            .ToList();
    }

    public async Task<PlantSampleModel?> GetById(string id)
    {
        var doc = await _db
            .Collection("plantSamples")
            .Document(id)
            .GetSnapshotAsync();

        if (!doc.Exists)
            return null;

        return doc.ConvertTo<PlantSampleModel>();
    }
}