using BloggBlazorServer.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<UserStateService>();
builder.Services.AddHttpClient<SearchService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7018");
});



// Registrer HttpClient
builder.Services.AddScoped<HttpClient>(s =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://localhost:7018") };
    return client;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();