namespace WebOrders_PW.Entity;

public class Payment
{
    public string CardType { get; set; }   // Visa / MasterCard / American Express
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; } // MM/YY
}
