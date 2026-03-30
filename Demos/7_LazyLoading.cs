using clase5_codefirst.Data;

namespace clase5_codefirst.Demos;

public static class Demo7_LazyLoading
{
    public static void Run(ApplicationDbContext context)
    {
        Console.WriteLine("> DEMO 7: Estrategia - Lazy Loading (Carga Perezosa Automática)");
        Console.WriteLine(
            "La magia negra de 'virtual' y UseLazyLoadingProxies. Interactuar con las propiedades dispara queries silenciosas y mágicas.\n"
        );

        var user = context.Users.OrderBy(u => u.Id).Skip(1).FirstOrDefault(); // Query 1

        if (user != null)
        {
            Console.WriteLine(
                $"Tenemos al usuario {user.Rut}. Aún no se traen los pedidos de SQL Server, fíjate en los logs."
            );

            // Justo en el momento de tocar .Count, veremos nacer la query en los logs (Interceptador de Proxy)
            int count = user.Orders.Count;

            Console.WriteLine(
                $"Query inyectada exitosamente de forma invisible. Cantidad revelada: {count} pedidos."
            );
        }
    }
}
