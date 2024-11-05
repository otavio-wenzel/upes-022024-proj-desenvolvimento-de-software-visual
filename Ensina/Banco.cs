

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {

    protected override void OnConfiguring(
        DbContextOptionsBuilder builder)
    {
        string con = "server=localhost;port=3306;" +
                     "database=escola;user=root;password=positivo";

        builder.UseMySQL(con)
        .LogTo(Console.WriteLine, LogLevel.Information);
        
    }

    //Tabelas
    public DbSet<Aluno> Alunos => Set<Aluno>();

}