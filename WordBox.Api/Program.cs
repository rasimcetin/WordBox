using Microsoft.EntityFrameworkCore;
using WordBox.Api;
using WordBox.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<WordBoxDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WordBoxDB")));

builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IWordMeaningService, WordMeaningService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
