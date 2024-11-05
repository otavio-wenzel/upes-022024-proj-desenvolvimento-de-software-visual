using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configurações Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configuração Entity Framework para uso do MySQL
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

//Configurações Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Planner API");

app.MapGet("/tarefas", async (AppDbContext db) =>
    await db.Tarefas.ToListAsync());


app.MapGet("/tarefas/{id}", async (int id, AppDbContext db) => 
    await db.Tarefas.FindAsync(id)
      is Tarefa tarefa
        ? Results.Ok(tarefa)
          : Results.NotFound());
    

app.MapPost("/tarefas", async (Tarefa tarefa, AppDbContext db) => {
    db.Tarefas.Add(tarefa);
    await db.SaveChangesAsync();
    return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
});

app.MapPut("tarefas/{id}", async (int id, Tarefa tarefaAlterada, AppDbContext db) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa is null) return Results.NotFound();

    tarefa.Descricao = tarefaAlterada.Descricao;
    tarefa.Concluida = tarefaAlterada.Concluida;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("tarefas/{id}", async (int id, AppDbContext db) =>
{
    if(await db.Tarefas.FindAsync(id) is Tarefa tarefa){

        db.Tarefas.Remove(tarefa);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
 
});

//Aluno
app.MapGet("/alunos", async (AppDbContext db) =>
    await db.Alunos.ToListAsync());

app.MapGet("/alunos/{id}", async (int id, AppDbContext db) => 
    await db.Alunos.FindAsync(id)
      is Aluno aluno
        ? Results.Ok(aluno)
          : Results.NotFound());

app.MapPost("/alunos", async (Aluno aluno, AppDbContext db) => {
    db.Alunos.Add(aluno);
    await db.SaveChangesAsync();
    return Results.Created($"/alunos/{aluno.Id}", aluno);
});

app.MapPut("alunos/{id}", async (int id, Aluno alunoAtualizado, AppDbContext db) =>
{
    var aluno = await db.Alunos.FindAsync(id);
    if (aluno is null) return Results.NotFound();

    aluno.Nome = alunoAtualizado.Nome;
    aluno.DataDeNascimento = alunoAtualizado.DataDeNascimento;
    aluno.Rg = alunoAtualizado.Rg;
    aluno.Telefone = alunoAtualizado.Telefone;
    aluno.Email = alunoAtualizado.Email;
    aluno.NomeDoTutor = alunoAtualizado.NomeDoTutor;
    aluno.RgDoTutor = alunoAtualizado.RgDoTutor;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("alunos/{id}", async (int id, AppDbContext db) =>
{
    if(await db.Alunos.FindAsync(id) is Aluno aluno){

        db.Alunos.Remove(aluno);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
 
});


app.Run();
