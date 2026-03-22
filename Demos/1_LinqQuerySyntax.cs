using System.Linq;
using clase5_codefirst.Data;

namespace clase5_codefirst.Demos;

public static class Demo1_LinqQuerySyntax
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 1: LINQ Query Syntax (Estilo SQL Clásico)");
        Console.WriteLine(
            "Ideal para ver cómo LINQ imita el estándar internacional de bases de datos.\n"
        );

        var query =
            from u in context.Users
            where u.BirthDate.Year < 2000
            orderby u.BirthDate ascending
            select new { u.Rut, u.BirthDate };

        // ToList() fuerza a EF Core a ejecutar la query real contra SQLite
        foreach (var user in query.Take(3).ToList())
        {
            Console.WriteLine($"Usuario: {user.Rut}");
        }
    }
}
