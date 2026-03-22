using clase5_codefirst.Models;
using FluentValidation;

namespace clase5_codefirst.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        // Validación personalizada 3 (FluentValidation)
        RuleFor(p => p.Amount)
            .Must(ValidAmount)
            .WithMessage(
                "El monto del pedido debe ser estrcitamente mayor a 0 y menor a 1,000,000."
            );

        // Validación personalizada 4 (FluentValidation)
        RuleFor(p => p.Date)
            .Must(BusinessDay)
            .WithMessage("El pedido debe realizarse en un día hábil (lunes a viernes).");
    }

    private bool ValidAmount(decimal amount)
    {
        return amount > 0 && amount < 1000000;
    }

    private bool BusinessDay(DateTime date)
    {
        return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }
}
