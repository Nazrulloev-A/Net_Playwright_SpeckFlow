using Bogus;
using Bogus.DataSets;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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







    public async Task  ClickOnLinkButton()
    {
        await ThisLinkBtn.ClickAsync();
    }

    public async Task FillOrderInformation()
    {

        await ProductSelct.SelectOptionAsync(new[] { new SelectOptionValue() { Value = "FamilyAlbum" } });
        await QuantityInput.FillAsync(_faker.Random.Int(1,10).ToString());
        await PricePerUnitInput.FillAsync(_faker.Random.Int(99,100).ToString());
        await DiscountInput.FillAsync(_faker.Random.Int(1,100).ToString());
        await TotalInput.FillAsync(_faker.Random.Int(1,9).ToString());
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

        var num = (Regex.Replace(_faker.Finance.CreditCardNumber(CardType.Visa), @"\D", ""));

        foreach (char c in num)

        {

           await CardNumberInput.FillAsync(num);

        }

        await ExpireDateInput.FillAsync(_faker.Date.Future(3).ToString("MM/yy"));
        await ProcessBtn.ClickAsync();
    }

}


