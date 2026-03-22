using System.ComponentModel.DataAnnotations;

namespace clase5_codefirst.Validations;

public class ValidRutAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var rut = value as string;
        if (string.IsNullOrEmpty(rut))
        {
            return new ValidationResult("El RUT no puede estar vacío.");
        }

        if (!rut.Contains('-'))
        {
            return new ValidationResult("El RUT debe contener un guion (-).");
        }

        return ValidationResult.Success;
    }
}
