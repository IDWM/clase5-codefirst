using System.Linq;
using clase5_codefirst.Data;
using Microsoft.EntityFrameworkCore;

namespace clase5_codefirst.Demos;

public static class Demo5_EagerLoading
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 5: Estrategia - Eager Loading (Carga Temprana)");
        Console.WriteLine(
            "Se utiliza el método '.Include()'. Resuelve toda la tabla empaquetada inicialmente en un gigantesco LEFT JOIN en SQL.\n"
        );

        var user = context.Users.Include(u => u.Orders).OrderBy(u => u.Id).FirstOrDefault();

        Console.WriteLine(
            $"Listo! El usuario {user?.Rut} extrajo sus {user?.Orders.Count} pedidos desde el primer instante."
        );
    }
}
