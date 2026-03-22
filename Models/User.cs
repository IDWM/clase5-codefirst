using System.ComponentModel.DataAnnotations;
using clase5_codefirst.Validations;

namespace clase5_codefirst.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El RUT es obligatorio")]
    [ValidRut] // Validación personalizada 1 (DataAnnotation)
    public string Rut { get; set; } = string.Empty;

    [Required]
    [LegalAge] // Validación personalizada 2 (DataAnnotation)
    public DateTime BirthDate { get; set; }

    // Propiedad de Navegación (Un Usuario tiene Muchos Pedidos)
    public virtual List<Order> Orders { get; set; } = new List<Order>();
}
