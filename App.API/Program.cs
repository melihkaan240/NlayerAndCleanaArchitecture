using App.Repositories;
using App.Repositories.Extensions;
using App.Services.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS ayarlar�n� ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()    // T�m kaynaklara izin verir
              .AllowAnyMethod()    // T�m HTTP metodlar�na izin verir
              .AllowAnyHeader();   // T�m ba�l�klara izin verir
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
app.UseCors("AllowAllOrigins");  // CORS'u etkinle�tiriyoruz

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
