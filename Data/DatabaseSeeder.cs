using System.Text;
using Bogus;
using clase5_codefirst.Models;
using FluentValidation;

namespace clase5_codefirst.Data;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context, IValidator<Order> orderValidator)
    {
        if (!context.Users.Any())
        {
            // Configuración de Generador de Usuarios
            var userFaker = new Faker<User>()
                .RuleFor(
                    u => u.Rut,
                    f => $"{f.Random.Number(10000000, 25000000)}-{f.Random.Number(0, 9)}"
                )
                .RuleFor(u => u.BirthDate, f => f.Date.Past(40, DateTime.Now.AddYears(-18))); // Usuarios con mínimo 18 años

            // Configuración de Generador de Pedidos
            var orderFaker = new Faker<Order>()
                .RuleFor(o => o.Amount, f => f.Finance.Amount(1000, 500000))
                .RuleFor(
                    o => o.Date,
                    f =>
                    {
                        // Obtener una fecha reciente y asegurarse de que sea día hábil
                        var date = f.Date.Recent(30).Date;
                        while (
                            date.DayOfWeek == DayOfWeek.Saturday
                            || date.DayOfWeek == DayOfWeek.Sunday
                        )
                        {
                            date = date.AddDays(1);
                        }
                        return date;
                    }
                );

            // Generar 50 Usuarios
            var users = userFaker.Generate(50);

            // Generar entre 1 y 5 pedidos por usuario
            foreach (var user in users)
            {
                var orders = orderFaker.GenerateBetween(1, 5);
                user.Orders = orders;
            }

            var validationErrors = new List<string>();
            for (var userIndex = 0; userIndex < users.Count; userIndex++)
            {
                var user = users[userIndex];

                var userValidationResults =
                    new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(
                    user
                );
                var isUserValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(
                    user,
                    validationContext,
                    userValidationResults,
                    validateAllProperties: true
                );

                if (!isUserValid)
                {
                    foreach (var validationResult in userValidationResults)
                    {
                        validationErrors.Add(
                            $"User[{userIndex}] RUT '{user.Rut}': {validationResult.ErrorMessage}"
                        );
                    }
                }

                for (var orderIndex = 0; orderIndex < user.Orders.Count; orderIndex++)
                {
                    var order = user.Orders[orderIndex];
                    var orderValidation = orderValidator.Validate(order);
                    if (!orderValidation.IsValid)
                    {
                        foreach (var error in orderValidation.Errors)
                        {
                            validationErrors.Add(
                                $"User[{userIndex}] Order[{orderIndex}] ({error.PropertyName}): {error.ErrorMessage}"
                            );
                        }
                    }
                }
            }

            if (validationErrors.Count > 0)
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendLine("Seed validation failed:");
                foreach (var error in validationErrors)
                {
                    messageBuilder.AppendLine($"- {error}");
                }
                throw new System.ComponentModel.DataAnnotations.ValidationException(
                    messageBuilder.ToString()
                );
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
