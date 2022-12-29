using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Repositories;
using BE_LosQuebrachosApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
                         builder => builder.AllowAnyOrigin()
                                           .AllowAnyHeader()
                                           .AllowAnyMethod()));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IChoferRepository, ChoferRepository>();
builder.Services.AddScoped<IOrdenDeCargaRepository, OrdenDeCargaRepository>();
builder.Services.AddScoped<IVehiculoRepsitory, VehiculoRepository>();
builder.Services.AddScoped<ITransporteRepository, TransporteRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IOrdenDeGasoilRepository, OrdenDeGasoilRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
