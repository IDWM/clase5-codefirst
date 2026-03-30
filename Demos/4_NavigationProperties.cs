using clase5_codefirst.Data;

namespace clase5_codefirst.Demos;

public static class Demo4_NavigationProperties
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 4: Propiedades de Navegación");
        Console.WriteLine(
            "El diseño puro de EF Core: Acceder a listas como 'u.Orders' hace que EF elabore los JOINs SQL invisibles por debajo.\n"
        );

        var query = context
            .Users.Where(u => u.Orders.Any()) // EF Core traducirá esto usando EXISTS o INNER JOIN
            .OrderBy(u => u.Id)
            .Select(u => new { u.Rut, TotalGastado = u.Orders.Sum(o => o.Amount) })
            .Take(3);

        foreach (var item in query.ToList())
        {
            Console.WriteLine($"Usuario: {item.Rut} | Gastó Históricamente: {item.TotalGastado:C}");
        }
    }
}
