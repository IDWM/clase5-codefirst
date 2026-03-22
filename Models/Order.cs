namespace clase5_codefirst.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; } // Llave Foránea Estricta C#

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    // Propiedad de Navegación (Un Pedido pertenece a Un Usuario)
    public virtual User? User { get; set; }
}
