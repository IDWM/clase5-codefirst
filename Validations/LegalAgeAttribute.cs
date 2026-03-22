using System.ComponentModel.DataAnnotations;

namespace clase5_codefirst.Validations;

public class LegalAgeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                return new ValidationResult("El usuario debe ser mayor de 18 años.");
            }
        }
        return ValidationResult.Success;
    }
}
