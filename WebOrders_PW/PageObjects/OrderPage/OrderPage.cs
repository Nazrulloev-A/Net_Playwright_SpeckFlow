using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;



namespace WebOrders_PW.PageObjects.OrderPage;

public class OrderPage
{
    private readonly BasePage _basePage;
    private readonly IPage _page;
    private readonly IConfiguration _config;
    
    
    
    public OrderPage(IPage page, IConfiguration config)
    {
        _page = page;
        _config = config;

    }
    
    private ILocator TitlePage => _page.Locator("h1");
    private ILocator ListOfAllOrders => _page.Locator("//table[@id=\"ctl00_MainContent_orderGrid\"]//tr ").Nth(1);
    private ILocator CheckAllBtn => _page.Locator("#ctl00_MainContent_btnCheckAll");
    private ILocator UnCheckBtn => _page.Locator("#ctl00_MainContent_btnUnCheck");
    private ILocator DeleteBtn => _page.Locator("//input[@name='ctl00$MainContent$btnDelete']");
    private ILocator EmptyListMessage => _page.Locator("#ctl00_MainContent_orderMessage");
    private ILocator NewLikBtn => _page.Locator("//a[@id='ctl00_MainContent_orderLInk']");


    public async Task ValidateTitle()
    {
        await TitlePage.IsVisibleAsync();
        await Assertions.Expect(TitlePage).ToHaveTextAsync(new Regex("Web Orders"));
    }

    public async Task ValidateListOfAllOrders()
    {
        await ListOfAllOrders.IsVisibleAsync();
        await Assertions.Expect(ListOfAllOrders).ToBeVisibleAsync();
    }

    public async Task ClickCheckAllBtn()
    {
        await CheckAllBtn.ClickAsync();
        
    }

    public async Task ClickOnDeleteBtn()
    {
        await DeleteBtn.ClickAsync();
    }

    public async Task EmptyListMessageValidation()
    {
        await EmptyListMessage.IsVisibleAsync();
        await Assertions.Expect(EmptyListMessage).ToHaveTextAsync(new Regex("List of orders is empty. In order to add new order use this link."));
        
    }

    public async Task ClickOnLink()
    {
        await NewLikBtn.ClickAsync();
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}