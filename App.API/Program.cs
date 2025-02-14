using App.Repositories;
using App.Repositories.Extensions;
using App.Services.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS ayarlarýný ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()    // Tüm kaynaklara izin verir
              .AllowAnyMethod()    // Tüm HTTP metodlarýna izin verir
              .AllowAnyHeader();   // Tüm baþlýklara izin verir
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS middleware'ini uygulamaya ekliyoruz
app.UseCors("AllowAllOrigins");  // CORS'u etkinleþtiriyoruz

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
