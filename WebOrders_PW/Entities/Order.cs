using Bogus.DataSets;

namespace WebOrders_PW.Entity;

public class Order
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Discount { get; set; }

    public Address Address { get; set; }
    public Payment Payment { get; set; }
}
