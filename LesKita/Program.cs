using Blazored.LocalStorage;
using DevExpress.Blazor;
using LesKita;
using LesKita.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<RefreshViewState>();
builder.Services.AddSingleton<ISvcStatusBar, svcStatusBar>();
builder.Services.AddSingleton<ClsServisSignalR>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ClsServisDrive>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    var config = builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = config["ClientId"];
    options.ClientSecret = config["ClientSecret"];
    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.UseAntiforgery();
app.MapControllers();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapHub<SignalRHub>("/signalrhub");
app.MapGet("/login-google", async context =>
{
    await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = "/"
    });
});
app.MapPost("/upload-audio", async (HttpRequest request) =>
{
    var file = request.Form.Files["file"];
    if (file is null) return Results.BadRequest();

    var uploadsDir = Path.Combine(app.Environment.WebRootPath, "uploads");
    Directory.CreateDirectory(uploadsDir);

    var filePath = Path.Combine(uploadsDir, file.FileName);
    await using var stream = File.Create(filePath);
    await file.CopyToAsync(stream);

    // Kirim URL relatif ke Blazor
    return Results.Text($"/uploads/{file.FileName}");
});
app.Run();
