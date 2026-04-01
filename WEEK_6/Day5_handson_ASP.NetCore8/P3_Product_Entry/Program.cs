var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // enable session

var app = builder.Build();

app.UseSession(); // use session

app.MapDefaultControllerRoute();
app.Run();