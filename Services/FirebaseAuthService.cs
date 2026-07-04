using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using HomePlant.Models;
using HomePlant.ViewModels;

namespace HomePlant.Services;

public class FirebaseAuthService
{
    private readonly FirestoreDb _db;

    public FirebaseAuthService(
        FirestoreDb db)
    {
        _db = db;
    }

    public async Task<string> Register(
        RegisterVM model)
    {
        var userArgs = new UserRecordArgs()
        {
            Email = model.Email,
            Password = model.Password,
            DisplayName = model.FullName
        };

        UserRecord firebaseUser =
            await FirebaseAuth.DefaultInstance
            .CreateUserAsync(userArgs);

        var user = new UserModel
        {
            FullName = model.FullName,
            Email = model.Email,
            Phone = model.Phone,
            Role = "User",
            Status = "Active",
            CreatedAt = DateTime.Now
        };

        await _db.Collection("users")
            .Document(firebaseUser.Uid)
            .SetAsync(user);

        return firebaseUser.Uid;
    }

    public async Task<UserModel?> GetUser(
        string uid)
    {
        var doc =
            await _db.Collection("users")
            .Document(uid)
            .GetSnapshotAsync();

        if (!doc.Exists)
            return null;

        return doc.ConvertTo<UserModel>();
    }
}