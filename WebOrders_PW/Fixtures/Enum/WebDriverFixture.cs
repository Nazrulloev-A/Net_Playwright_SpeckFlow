using WebOrders_PW.Fixtures.BrowserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using WebOrders_PW.Fixtures.BrowserModel;
using Xunit;

namespace DemoFramewrok.Fixtures;

public class WebDriverFixture : IAsyncLifetime
{
    private readonly BrowserOptions _browserConfig;

    public WebDriverFixture(IConfiguration configuration)
    {
        _browserConfig = BrowserOptions.GetConfig(configuration);
    }

    public async Task InitializeAsync()
    {
        Browser = await WebDriverFactory.CreateDriver(_browserConfig.BrowserType, _browserConfig.Headless, _browserConfig.SlowMo);
        
    }
    public IBrowser Browser { get; private set; }
        

    public async Task DisposeAsync()
    {
        await Browser.CloseAsync();

    }
    

}
