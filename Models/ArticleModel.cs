using Google.Cloud.Firestore;

namespace HomePlant.Models;

[FirestoreData]
public class ArticleModel
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty]
    public string Title { get; set; }

    [FirestoreProperty]
    public string Content { get; set; }

    [FirestoreProperty]
    public string ThumbnailUrl { get; set; }

    [FirestoreProperty]
    public string Status { get; set; }

    [FirestoreProperty]
    public Timestamp CreatedAt { get; set; }
}