/*using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace WebOrders_PW.PageObjects;

public class AddNewOrder
{
    private readonly BasePage _basePage;
    private readonly IPage _page;
    private readonly IConfiguration _config;
    private readonly Faker _faker = new Faker("en_US");


    public AddNewOrder(IPage page, IConfiguration config)
    {
        _page = page;
        _config = config;


    }

    private ILocator ThisLinkBtn => _page.Locator("#ctl00_MainContent_orderLInk");

    //Product Information
    private ILocator ProductSelct => _page.Locator("#ctl00_MainContent_fmwOrder_ddlProduct");
    private ILocator QuantityInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtQuantity");
    private ILocator PricePerUnitInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtUnitPrice");
    private ILocator DiscountInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtDiscount");
    private ILocator TotalInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtTotal");
    private ILocator CalculateBtn => _page.Locator("//input[@type='submit']");

    //Address Information
    private ILocator CustomerNameInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtName");
    private ILocator StreetInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox2");
    private ILocator CityInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox3");
    private ILocator StateInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox4");
    private ILocator ZipInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox5");

    //Payment Information
    private ILocator CardVisa => _page.Locator("#ctl00_MainContent_fmwOrder_cardList_0");
    private ILocator CardMaster => _page.Locator("#ctl00_MainContent_fmwOrder_cardList_1");
    private ILocator CardAmericanExpress => _page.Locator("#ctl00_MainContent_fmwOrder_cardList_2");
    private ILocator CardNumberInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox6");
    private ILocator ExpireDateInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox1");
    private ILocator ProcessBtn => _page.Locator("#ctl00_MainContent_fmwOrder_InsertButton");
    private ILocator ResetBtn => _page.Locator("//input[@type='reset']");
    public ILocator SuccessOrderText => _page.Locator("//strong[contains(text(), 'New order has been successfully added')]");







    public async Task ClickOnLinkButton()
    {
        await ThisLinkBtn.ClickAsync();
    }

    public async Task FillOrderInformation()
    {

        await ProductSelct.SelectOptionAsync(new[] { new SelectOptionValue() { Value = "FamilyAlbum" } });
        await QuantityInput.FillAsync(_faker.Random.Int(1, 10).ToString());
        await PricePerUnitInput.FillAsync(_faker.Random.Int(99, 100).ToString());
        await DiscountInput.FillAsync(_faker.Random.Int(1, 100).ToString());
        await TotalInput.FillAsync(_faker.Random.Int(1, 9).ToString());
        await CalculateBtn.ClickAsync();

    }

    public async Task FillAddressInformation()
    {
        await CustomerNameInput.FillAsync(_faker.Name.FirstName());
        await StreetInput.FillAsync(_faker.Address.State());
        await CityInput.FillAsync(_faker.Address.City());
        await StateInput.FillAsync(_faker.Address.StateAbbr());
        var zip = _faker.Address.ZipCode("#####");
        await ZipInput.FillAsync(zip);
    }

    public async Task CreditCardInformation()
    {
        await CardVisa.SetCheckedAsync(true);



        var boundingBox = await CardNumberInput.BoundingBoxAsync();

        if (boundingBox != null)
        {
            await _page.WaitForTimeoutAsync(1000);

            //access the x and y properties 
            var absoluteX = boundingBox.X;
            var absoluteY = boundingBox.Y;

            await _page.Mouse.MoveAsync((float)absoluteX + 20, (float)absoluteY + 15);
            await _page.Mouse.ClickAsync((float)absoluteX + 20, (float)absoluteY + 15);
            Console.WriteLine($"Element's absolute X coordinate: {absoluteX}");
            Console.WriteLine($"Element's absolute Y coordinate: {absoluteY}");
            var num = (Regex.Replace(_faker.Finance.CreditCardNumber(CardType.Visa), @"\D", ""));

            foreach (char c in num)
            {
                await CardNumberInput.Page.Keyboard.TypeAsync(c.ToString());
                await Task.Delay(1000);
            }

        }

        else
        {
            Console.WriteLine("Could not find the elemet or determine it's bounding box");
        }
        await ExpireDateInput.FillAsync(_faker.Date.Future(3).ToString("MM/yy"));
        await ProcessBtn.ClickAsync();
    }

} */

using Microsoft.Playwright;
using WebOrders_PW.Entity;

namespace WebOrders_PW.PageObjects;

public class AddNewOrder
{
    private readonly IPage _page;

    public AddNewOrder(IPage page)
    {
        _page = page;
    }

    private ILocator ThisLinkBtn => _page.Locator("#ctl00_MainContent_orderLInk");

    // Product
    private ILocator ProductSelect => _page.Locator("#ctl00_MainContent_fmwOrder_ddlProduct");
    private ILocator QuantityInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtQuantity");
    private ILocator PricePerUnitInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtUnitPrice");
    private ILocator DiscountInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtDiscount");
    private ILocator CalculateBtn => _page.Locator("//input[@type='submit']");

    // Address
    private ILocator CustomerNameInput => _page.Locator("#ctl00_MainContent_fmwOrder_txtName");
    private ILocator StreetInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox2");
    private ILocator CityInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox3");
    private ILocator StateInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox4");
    private ILocator ZipInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox5");

    // Payment
    private ILocator CardVisa => _page.Locator("#ctl00_MainContent_fmwOrder_cardList_0");
    private ILocator CardMaster => _page.Locator("#ctl00_MainContent_fmwOrder_cardList_1");
    private ILocator CardAmericanExpress => _page.Locator("#ctl00_MainContent_fmwOrder_cardList_2");
    private ILocator CardNumberInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox6");
    private ILocator ExpireDateInput => _page.Locator("#ctl00_MainContent_fmwOrder_TextBox1");
    private ILocator ProcessBtn => _page.Locator("#ctl00_MainContent_fmwOrder_InsertButton");

    public async Task ClickOnLinkButton()
    {
        await ThisLinkBtn.ClickAsync();
    }

    public async Task FillOrderInformation(Order order)
    {
        await ProductSelect.SelectOptionAsync(order.Product);
        await QuantityInput.FillAsync(order.Quantity.ToString());
        await PricePerUnitInput.FillAsync(order.PricePerUnit.ToString());
        await DiscountInput.FillAsync(order.Discount.ToString());
        await CalculateBtn.ClickAsync();
    }

    public async Task FillAddressInformation(Order order)
    {
        await CustomerNameInput.FillAsync(order.Address.CustomerName);
        await StreetInput.FillAsync(order.Address.Street);
        await CityInput.FillAsync(order.Address.City);
        await StateInput.FillAsync(order.Address.State);
        await ZipInput.FillAsync(order.Address.Zip);
    }

    public async Task FillPaymentInformation(Order order)
    {
        switch (order.Payment.CardType)
        {
            case "Visa":
                await CardVisa.SetCheckedAsync(true);
                break;
            case "MasterCard":
                await CardMaster.SetCheckedAsync(true);
                break;
            case "American Express":
                await CardAmericanExpress.SetCheckedAsync(true);
                break;
        }

        await CardNumberInput.FillAsync(order.Payment.CardNumber);
        await ExpireDateInput.FillAsync(order.Payment.ExpiryDate);
        await ProcessBtn.ClickAsync();
    }
}

