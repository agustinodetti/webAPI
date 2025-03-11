using apiGPT.Services;
using Microsoft.OpenApi.Models;
using Google.Cloud.Translation.V2;


var builder = WebApplication.CreateBuilder(args);

// Cargar la clave API desde el archivo de configuración
var apiKey = builder.Configuration["GoogleCloud:ApiKey"];
builder.Services.AddSingleton(TranslationClient.CreateFromApiKey(apiKey));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Agregar el servicio de OpenAI
builder.Services.AddSingleton<OpenAIService>();

// Agregar servicios a la aplicación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ChatGPT API",
        Version = "v1",
        Description = "API que consume OpenAI ChatGPT"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configurar Swagger solo en modo desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatGPT API v1");
        c.RoutePrefix = string.Empty; // Para abrir Swagger en la raíz (localhost:5000)
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
