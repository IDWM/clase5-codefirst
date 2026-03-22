using System.Linq;
using clase5_codefirst.Data;
using Microsoft.EntityFrameworkCore;

namespace clase5_codefirst.Demos;

public static class Demo6_ExplicitLoading
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 6: Estrategia - Explicit Loading (Carga Explícita a Demanda)");
        Console.WriteLine(
            "Solo carga los datos relacionales extra si la regla de código entra ahí y manualmene lo pedimos con '.Load()'.\n"
        );

        var user = context.Users.OrderBy(u => u.Id).LastOrDefault(); // Carga básica

        if (user != null)
        {
            Console.WriteLine(
                $"Usuario detectado: {user.Rut}. Sus pedidos no están cargados. Lanzaremos la segunda instrucción individual."
            );

            // Obligamos al ORM a viajar a SQL por los detalles solo de esta persona
            context.Entry(user).Collection(u => u.Orders).Load();

            Console.WriteLine(
                $"Ahora sí, colección cargada. Total de Pedidos: {user.Orders.Count}"
            );
        }
    }
}
