using ProyectoLenguajesBaseDatos.OracleDbContext;
using ProyectoLenguajesBaseDatos.Service.ServiceImplement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add sericices DB and interface => Singleton 
builder.Services.AddScoped<Context>(options => new Context(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<NoticiaImplement>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Noticia}/{action=Listado}/{id?}");

app.Run();
