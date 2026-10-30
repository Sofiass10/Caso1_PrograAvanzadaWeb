using Albums.Abstract.Interfaces.DA;
using Albums.Abstract.Interfaces.FL;
using Albums.DataAccess;
using Albums.Flujo.Flujos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAlbumDA, AlbumnesDA>();
builder.Services.AddScoped<IAlbumFlujo, AlbumFlujo>();

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();