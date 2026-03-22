using System.Linq;
using clase5_codefirst.Data;

namespace clase5_codefirst.Demos;

public static class Demo2_LinqMethodSyntax
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 2: LINQ Method Syntax (Estilo Moderno con Lambdas)");
        Console.WriteLine(
            "Ambas sintaxis compilan exactamente igual, pero Lambda es el estándar moderno en C#.\n"
        );

        var query = context
            .Users.Where(u => u.BirthDate.Year < 2000)
            .OrderBy(u => u.BirthDate)
            .Select(u => new { u.Rut, u.BirthDate })
            .Take(3);

        foreach (var user in query.ToList())
        {
            Console.WriteLine($"Usuario: {user.Rut}");
        }
    }
}
