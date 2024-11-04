using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configurações Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configuração Entity Framework em memória
// builder.Services.AddDbContext<AppDbContext>(
//     options => options.UseInMemoryDatabase("Tarefas")
// );
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//----------------------------

//Configuração Entity Framework para uso do MySQL
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

//Configurações Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Planner API");

//1a OPÇÃO
//app.MapGet("/tarefas", ()=> "Retornar as tarefas");

//2a OPÇÃO
// string FuncTarefas() {
//     return "Retornar as tarefas função convencional";
// }
// app.MapGet("/tarefas", FuncTarefas);

//3a OPÇÃO
// app.MapGet("/tarefas", ()=> {
//     return Banco.getTarefas();
// });

//4a OPÇÃO
// app.MapGet("/tarefas", ()=> Banco.getTarefas());

//5a OPÇÃO
//app.MapGet("/tarefas", Banco.getTarefas);

app.MapGet("/tarefas", async (AppDbContext db) =>
    await db.Tarefas.ToListAsync());


//app.MapGet("/tarefas/{id}", (int id) => 
//    Banco.getTarefas().FirstOrDefault(t => t.Id == id)

app.MapGet("/tarefas/{id}", async (int id, AppDbContext db) => 
    await db.Tarefas.FindAsync(id)
      is Tarefa tarefa
        ? Results.Ok(tarefa)
          : Results.NotFound());
    
// app.MapPost("/tarefas", (Tarefa tarefa) => {
//     List<Tarefa> tarefas = Banco.getTarefas();
//     tarefa.Id = tarefas.Count + 1;
//     tarefas.Add(tarefa);
//     return tarefa;
// });

app.MapPost("/tarefas", async (Tarefa tarefa, AppDbContext db) => {
    db.Tarefas.Add(tarefa);
    await db.SaveChangesAsync();
    return Results.Created($"/tarefas/{tarefa.Id}", tarefa);
});

// app.MapPut("tarefas/{id}", (int id, Tarefa tarefa) =>
// {
//     List<Tarefa> tarefas = Banco.getTarefas();
//     var tarefaExiste = tarefas.FirstOrDefault(t =>
//         t.Id == id
//     );
//     if (tarefaExiste == null){
//         return Results.NotFound();
//     }
//     tarefaExiste.Descricao = tarefa.Descricao;
//     tarefaExiste.Concluida = tarefa.Concluida;
//     return Results.Ok(tarefaExiste);
// });

app.MapPut("tarefas/{id}", async (int id, Tarefa tarefaAlterada, AppDbContext db) =>
{
    var tarefa = await db.Tarefas.FindAsync(id);
    if (tarefa is null) return Results.NotFound();

    tarefa.Descricao = tarefaAlterada.Descricao;
    tarefa.Concluida = tarefaAlterada.Concluida;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// app.MapDelete("tarefas/{id}", (int id) =>
// {
//     List<Tarefa> tarefas = Banco.getTarefas();
//     var tarefaExiste = tarefas.FirstOrDefault(t =>
//         t.Id == id
//     );
//     if (tarefaExiste == null){
//         return Results.NotFound();
//     }
//     tarefas.Remove(tarefaExiste);
//     return Results.NoContent();
// });

app.MapDelete("tarefas/{id}", async (int id, AppDbContext db) =>
{
    if(await db.Tarefas.FindAsync(id) is Tarefa tarefa){

        db.Tarefas.Remove(tarefa);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
 
});





app.MapGet("/produtos", () => "Produtos");
app.MapGet("/pessoas", () => "Pessoas");
app.MapGet("/pessoas/{id}", () => "Pessoa 1");

app.MapPost("/pessoas", () => "POST pessoa");
app.MapPost("/produtos", () => "POST produto");

app.MapPut("/pessoas/{id}", () => "PUT pessoa");
app.MapPut("/produtos/{id}", () => "PUT produto");

app.MapDelete("/pessoas/{id}", () => "DELETE pessoa");
app.MapDelete("/produtos/{id}", () => "DELETE produto");

app.Run();
