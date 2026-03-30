using clase5_codefirst.Data;
using Microsoft.EntityFrameworkCore;

namespace clase5_codefirst.Demos;

public static class Demo10_SqlInjection
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 10: Inyección SQL y FromSqlRaw");
        Console.WriteLine("Nunca debes concatenar cadenas si corres código crudo SQL Server.\n");

        // Payload inyectado, anulando un verificador (Omitiendo OR 1=1 por un always true válido en SQLite)
        // Ojo que SQLite evalúa '1'='1' en texto
        string userInput = "123' OR '1'='1";

        Console.WriteLine($"> Preparando atacante desde Form: {userInput}");

        try
        {
            var injection = context
                .Users.FromSqlRaw("SELECT * FROM Users WHERE Rut = '" + userInput + "'")
                .ToList();

            Console.WriteLine(
                $"\n[PELIGRO] La base de datos aceptó el Or Verdadero: ¡Botó {injection.Count} usuarios de la tabla sin filtros formales!"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
