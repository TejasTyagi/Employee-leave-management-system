//noahb
using Employee_leave_management_system.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting(); // Enable routing middleware - Tejas

app.UseDefaultFiles();

app.UseStaticFiles();   
app.MapControllers();

using (var db = new Database())
{
    db.Database.EnsureCreated();
}

app.Run();
