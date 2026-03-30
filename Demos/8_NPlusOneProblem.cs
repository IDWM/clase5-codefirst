using clase5_codefirst.Data;

namespace clase5_codefirst.Demos;

public static class Demo8_NPlusOneProblem
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 8: El Antipatrón N+1 Algorítmico");
        Console.WriteLine(
            "Un error catastrófico demostrando que el ORM es ciego dentro de bucles si no usas Include().\n"
        );

        // El '1' del problema
        var users = context.Users.OrderBy(u => u.Id).Take(5).ToList();

        Console.WriteLine(
            $"Iteraremos sobre los {users.Count} usuarios capturados e iremos a buscar sus órdenes a SQL dentro de cada vuelta del bucle...\n"
        );

        foreach (var user in users)
        {
            // El 'N' del problema (Lanzando una query completamente independiente hacia la red SQL Server por cada uno)
            var userOrders = context.Orders.Where(o => o.UserId == user.Id).ToList();
            Console.WriteLine($"Usuario {user.Rut} detectado con {userOrders.Count} pedidos.");
        }

        Console.WriteLine(
            "\n[Revisa los logs de arriba] ¡Inundamos la base de datos de transacciones pequeñas en vez de haber hecho un Join grande al inicio!"
        );
    }
}
