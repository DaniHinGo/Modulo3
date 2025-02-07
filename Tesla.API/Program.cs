using Microsoft.EntityFrameworkCore;
using Tesla.Business.Interfaces;
using Tesla.Business.Services;
using Tesla.Data.IRepository;
using Tesla.Data.Models;
using Tesla.Data.Repository;
using Tesla.Data.Repositoty;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<NikolaContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("NikolaDatabase"))
);

//Inyección de dependencias
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddScoped<IAlbumRepository<int, Album>, AlbumRepository<int, Album>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();