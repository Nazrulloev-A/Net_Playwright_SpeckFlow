using WebOrders_PW.Enum;
using WebOrders_PW.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using WebOrders_PW.PageObjects;
using Xunit;

namespace WebOrders_PW.PageObjects;

public class LoginPage
{
    private readonly BasePage _basePage;
    private readonly IPage _page;
    private readonly IConfiguration _config;

    public LoginPage(IPage page, IConfiguration config)
    {
        _page = page;
        _config = config;

    }
    
    private ILocator UserNameBox => _page.Locator("//input[@id='ctl00_MainContent_username']");
    private ILocator PasswordBox => _page.Locator("//input[@id='ctl00_MainContent_password']");
    private ILocator SignInBtn => _page.Locator("//input[@id='ctl00_MainContent_login_button']");
    private ILocator SingOutBtn => _page.Locator("//a[@id='ctl00_logout']");
    private ILocator ErrorMessage => _page.Locator("#ctl00_MainContent_status");
    
   
    
    
    public async Task UserLogin(string userRole)
    {
        string userName = null;
        string password = null;
        UserRoles userRoles;
        System.Enum.TryParse(userRole, ignoreCase: true, out userRoles);
        switch (userRoles)
        {
            case UserRoles.Staff:
                userName = ApplicationOptions.GetConfig(_config).StaffUser.Username;
                password = ApplicationOptions.GetConfig(_config).StaffUser.Password;
                break;
             case UserRoles.Invalid:
                userName = ApplicationOptions.GetConfig(_config).InvalidUser.Username;
                password = ApplicationOptions.GetConfig(_config).InvalidUser.Password;
                break;
            /*case UserRoles.Client:
                userName = ApplicationOptions.GetConfig(_config).ClientUser.Username;
                password = ApplicationOptions.GetConfig(_config).ClientUser.Password;
                break;
            case UserRoles.Client1:
                userName = ApplicationOptions.GetConfig(_config).ClientUser1.Username;
                password = ApplicationOptions.GetConfig(_config).ClientUser1.Password;
                break;
            case UserRoles.ClientFacilityTestUser:
                userName = ApplicationOptions.GetConfig(_config).PerDiemClientFacilityUser.Username;
                password = ApplicationOptions.GetConfig(_config).PerDiemClientFacilityUser.Password;
                break; */
        }

        await Login(userName, password);
        
    }

/*     public async Task UserLoginAsApiUser()
    {
        var userName = ApplicationOptions.GetConfig(_config).ApiUser.Username;
        var password = ApplicationOptions.GetConfig(_config).ApiUser.Password;

        await Login(userName, password);
    } */

    

    private async Task Login(string userName, string password)
    {
        if (await UserNameBox.CountAsync() > 0)
        {
            await UserNameBox.FillAsync(userName);
            await PasswordBox.FillAsync(password);
            await SignInBtn.ClickAsync();
        }
        else
        {
            await UserNameBox.FillAsync(userName);
            await PasswordBox.FillAsync(password);
            await SignInBtn.ClickAsync();
        }
       
    }



    public async Task LogOut()
    {
        await SingOutBtn.ClickAsync();
    }

    // Method to get the error message text
    public async Task<string> GetErrorMessageAsync()
    {
        // Wait for the error message to be visible to avoid race conditions
        await ErrorMessage.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        return await ErrorMessage.TextContentAsync() ?? string.Empty;
    }

    // Method to validate error message matches expected
    public async Task<bool> IsErrorMessageDisplayedAsync(string expectedMessage)
    {
        var actualMessage = await GetErrorMessageAsync();
        return actualMessage.Trim().Equals(expectedMessage, StringComparison.OrdinalIgnoreCase);
    }
    
    
    
    
}