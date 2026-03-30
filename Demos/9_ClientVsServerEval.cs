using clase5_codefirst.Data;

namespace clase5_codefirst.Demos;

public static class Demo9_ClientVsServerEval
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine(
            "> DEMO 9: Client Evaluation vs Server Evaluation (Antipatrón de Memoria)"
        );
        Console.WriteLine(
            "El ORM traduce nuestro C# a SQL. El lugar donde pones ToList() marca la gran frontera.\n"
        );

        Console.WriteLine("--- FORMA ERRÓNEA (Filtro C# en RAM Cliente) ---");
        // Traer absolutamente toda infraestructura a los microservidor
        var everythingInMemory = context.Users.ToList();
        // Luego filtrar consumiendo memoria
        var adultsMemory = everythingInMemory
            .Where(u => DateTime.Now.Year - u.BirthDate.Year >= 30)
            .ToList();
        Console.WriteLine(
            $"Se filtraron {adultsMemory.Count} usuarios, pero el Log SQL evidenció 'SELECT * FROM Users'..."
        );

        Console.WriteLine("\n--- FORMA CORRECTA (Filtro SQL en el Servidor) ---");
        // Mandar el Where para que forme parte del motor relacional
        var adultsDb = context
            .Users.Where(u => DateTime.Now.Year - u.BirthDate.Year >= 30)
            .ToList();
        Console.WriteLine(
            $"Se filtraron los mismos usuarios, pero el Motor de BD hizo el esfuerzo. Menor memoria y menor red."
        );
    }
}
