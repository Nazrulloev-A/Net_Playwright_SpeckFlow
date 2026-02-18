using Bogus;
using WebOrders_PW.Entity;

namespace WebOrders_PW.TestData;

public class OrderFaker
{
    public Faker<Order> GetOrderGenerator()
    {
        return new Faker<Order>()
            .RuleFor(o => o.Product, f => f.PickRandom(new[] { "MyMoney", "FamilyAlbum", "ScreenSaver" }))
            .RuleFor(o => o.Quantity, f => f.Random.Int(1, 10))
            .RuleFor(o => o.PricePerUnit, f => 100)
            .RuleFor(o => o.Discount, f => f.Random.Int(0, 20))

            .RuleFor(o => o.Address, f => new Address
            {
                CustomerName = f.Name.FullName(),
                Street = f.Address.StreetAddress(),
                City = f.Address.City(),
                State = f.Address.State(),
                Zip = f.Address.ZipCode()
            })

            .RuleFor(o => o.Payment, f => new Payment
            {
                CardType = f.PickRandom(new[] { "Visa", "MasterCard", "American Express" }),
                CardNumber = f.Finance.CreditCardNumber(),
                ExpiryDate = f.Date.Future(2).ToString("MM/yy")
            });
    }
}
