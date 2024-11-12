
using Microsoft.EntityFrameworkCore;

public static class AlunoApi
{
    public static void ConfigureAlunoApi(this WebApplication app)
    {
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
            //aluno.Telefone = alunoAtualizado.Telefone;
            //aluno.Email = alunoAtualizado.Email;
            //aluno.NomeDoTutor = alunoAtualizado.NomeDoTutor;
            //aluno.RgDoTutor = alunoAtualizado.RgDoTutor;

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
    }
}
