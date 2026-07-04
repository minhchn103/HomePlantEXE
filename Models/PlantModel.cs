using Google.Cloud.Firestore;

namespace HomePlant.Models;

[FirestoreData]
public class PlantModel
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty]
    public string UserId { get; set; }

    [FirestoreProperty]
    public string PlantSampleId { get; set; }

    [FirestoreProperty]
    public string Nickname { get; set; }

    [FirestoreProperty]
    public string Location { get; set; }

    [FirestoreProperty]
    public string CurrentStatus { get; set; }

    [FirestoreProperty]
    public string Note { get; set; }

    [FirestoreProperty]
    public Timestamp PurchaseDate { get; set; }

    [FirestoreProperty]
    public Timestamp CreatedAt { get; set; }
}