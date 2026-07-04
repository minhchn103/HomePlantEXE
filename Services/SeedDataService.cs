using Google.Cloud.Firestore;

public class SeedDataService
{
    private readonly FirestoreDb _db;

    public SeedDataService(FirestoreDb db)
    {
        _db = db;
    }

    public async Task SeedPlantSamples()
    {
        var plants = new[]
        {
            new
            {
                Id = "snake_plant",
                Name = "Cây Lưỡi Hổ",
                ScientificName = "Sansevieria trifasciata"
            },
            new
            {
                Id = "monstera",
                Name = "Monstera",
                ScientificName = "Monstera deliciosa"
            }
        };

        foreach (var p in plants)
        {
            await _db.Collection("plantSamples")
                .Document(p.Id)
                .SetAsync(new
                {
                    name = p.Name,
                    scientificName = p.ScientificName,
                    createdAt = Timestamp.GetCurrentTimestamp()
                });
        }
    }
}