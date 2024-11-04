

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {

    // public AppDbContext(
    //     DbContextOptions<AppDbContext> options)
    //      : base(options)
    // {
    // }

    protected override void OnConfiguring(
        DbContextOptionsBuilder builder)
    {
        string con = "server=localhost;port=3306;" +
                     "database=escola;user=root;password=positivo";

        builder.UseMySQL(con)
        .LogTo(Console.WriteLine, LogLevel.Information);
        
    }

    //Tabelas
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    public DbSet<Aluno> Alunos => Set<Aluno>();

}




/*public class Banco
{
    private static List<Tarefa> tarefas = new List<Tarefa>
    {
        new Tarefa { Id = 1, Descricao = "Estudar C#", Concluida = false },
        new Tarefa { Id = 2, Descricao = "Estudar ASP.NET Core", Concluida = false }
    };

    public static List<Tarefa> getTarefas()
    {
        return tarefas;
    }

    public static Tarefa getTarefa(int id)
    {
        return tarefas.FirstOrDefault(t => t.Id == id);
    }

    public static Tarefa addTarefa(Tarefa tarefa)
    {
        tarefa.Id = tarefas.Count + 1;
        tarefas.Add(tarefa);
        return tarefa;
    }

    public static Tarefa updateTarefa(int id, Tarefa tarefa)
    {
        var tarefaExistente = tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefaExistente == null)
        {
            return null;
        }

        tarefaExistente.Descricao = tarefa.Descricao;
        tarefaExistente.Concluida = tarefa.Concluida;
        return tarefaExistente;
    }

    public static bool deleteTarefa(int id)
    {
        var tarefaExistente = tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefaExistente == null)
        {
            return false;
        }

        tarefas.Remove(tarefaExistente);
        return true;
    }

}*/