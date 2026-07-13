using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using HomePlant.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
});

builder.Services.AddHttpContextAccessor();

var credentialPath = Path.Combine(
builder.Environment.ContentRootPath,
"Firebase",
"firebase-key.json");

var credential =
GoogleCredential.FromFile(credentialPath);

if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = credential
    });
}

builder.Services.AddSingleton(provider =>
{
    return new FirestoreDbBuilder
    {
        ProjectId = builder.Configuration["Firebase:ProjectId"],
        Credential = credential
    }.Build();
});

builder.Services.AddScoped<FirestoreService>();
builder.Services.AddScoped<PlantService>();
builder.Services.AddScoped<FirebaseAuthService>();
builder.Services.AddScoped<CareLogService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PlantSampleService>();
builder.Services.AddScoped<SeedDataService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var seed = scope.ServiceProvider
        .GetRequiredService<SeedDataService>();

    await seed.SeedPlantSamples();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
