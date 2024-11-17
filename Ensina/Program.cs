using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configurações Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configuração Entity Framework para uso do MySQL
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // URL do front-end
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

//Configurações Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowReactApp");

app.MapGet("/", () => "Tá rodando paiê!");

app.ConfigureAlunoApi();

app.Run();
