using ProyectoLenguajesBaseDatos.OracleDbContext;
using ProyectoLenguajesBaseDatos.Service.ServiceImplement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add sericices DB and interface => Singleton 
builder.Services.AddScoped<Context>(options => new Context(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<NoticiaImplement>();
builder.Services.AddScoped<UserImplement>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
