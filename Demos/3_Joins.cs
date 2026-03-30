using clase5_codefirst.Data;

namespace clase5_codefirst.Demos;

public static class Demo3_Joins
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 3: Relacionando Tablas (Joins Explícitos)");
        Console.WriteLine(
            "Usar el método .Join es engorroso en EF Core, pero estrictamente necesario si no existen propiedades de navegación.\n"
        );

        var query = context
            .Users.OrderBy(u => u.Id)
            .Join(
                context.Orders,
                u => u.Id, // Clave Primaria
                o => o.UserId, // Clave Foránea
                (u, o) => new { u.Rut, o.Amount }
            ) // Resultado Combinado
            .Take(3);

        foreach (var item in query.ToList())
        {
            Console.WriteLine($"Usuario: {item.Rut} - Su Pedido: {item.Amount:C}");
        }
    }
}
