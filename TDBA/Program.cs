using TDBA.Components;
using Microsoft.EntityFrameworkCore;
using TDBA.Data;
using TDBA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//my SQLite connection. 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//MY ORIGIONAL SQL SERVER CONNECTION
//builder.Services.AddDbContextFactory<AppDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//<-database connection in appsettings.json
builder.Services.AddScoped<EventService>();
builder.Services.AddHttpClient<WeatherService>(); //<-weather api service:)


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
