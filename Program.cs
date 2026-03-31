using clase5_codefirst.Data;
using clase5_codefirst.Models;
using clase5_codefirst.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options
        .UseLazyLoadingProxies()
        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();

var app = builder.Build();

// Generar base de datos automáticamente si no existe y aplicar migraciones
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var orderValidator = scope.ServiceProvider.GetRequiredService<IValidator<Order>>();
    context.Database.Migrate();

    // Poblar la base de datos con Bogus si está vacía
    DatabaseSeeder.Seed(context, orderValidator);
}

// Bloque de Ejecución de Demos por Consola
var demoArg = args.FirstOrDefault();
if (!string.IsNullOrEmpty(demoArg))
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    Console.WriteLine($"\n--- INICIANDO DEMO: {demoArg.ToUpper()} ---\n");
    switch (demoArg.ToLower())
    {
        case "1":
            clase5_codefirst.Demos.Demo1_LinqQuerySyntax.Run(context);
            break;
        case "2":
            clase5_codefirst.Demos.Demo2_LinqMethodSyntax.Run(context);
            break;
        case "3":
            clase5_codefirst.Demos.Demo3_Joins.Run(context);
            break;
        case "4":
            clase5_codefirst.Demos.Demo4_NavigationProperties.Run(context);
            break;
        case "5":
            clase5_codefirst.Demos.Demo5_EagerLoading.Run(context);
            break;
        case "6":
            clase5_codefirst.Demos.Demo6_ExplicitLoading.Run(context);
            break;
        case "7":
            clase5_codefirst.Demos.Demo7_LazyLoading.Run(context);
            break;
        case "8":
            clase5_codefirst.Demos.Demo8_NPlusOneProblem.Run(context);
            break;
        case "9":
            clase5_codefirst.Demos.Demo9_ClientVsServerEval.Run(context);
            break;
        case "10":
            clase5_codefirst.Demos.Demo10_SqlInjection.Run(context);
            break;
        default:
            Console.WriteLine($">>>> Demo '{demoArg}' no encontrado.");
            break;
    }
    Console.WriteLine($"\n--- FIN DEL DEMO ---\n");
    return; // Evita que arranque el servidor HTTP
}

app.MapControllers();

app.Run();
