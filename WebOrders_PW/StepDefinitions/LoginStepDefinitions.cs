using DemoFramewrok.PageObjects;
using Reqnroll;
using WebOrders_PW.PageObjects;

namespace WebOrders_PW.StepDefinitions;

[Binding]
public class LoginStepDefinitions
{
    private readonly BasePage _basePage;
    private readonly LoginPage _loginPage;
    private readonly HomePage _homePage;
    
    public LoginStepDefinitions(BasePage basePage)
    {
        _basePage = basePage;
        _loginPage = new LoginPage(_basePage.Page, _basePage.Config);
        _homePage = new HomePage(_basePage.Page, _basePage.Config);

    }


    [Given("user navigates to Test home page")]
    public async Task GivenUserNavigatesToTestHomePage()
    {
        await _basePage.Navigate();
        
    }

    [When("user logs in using username and password for Staff")]
    public async Task WhenUserLogsInUsingUsernameAndPasswordForStaff()
    {
        await _loginPage.UserLogin("Staff");
    }
    

    [Then("user successfully logged out")]
    public async Task ThenUserSuccessfullyLoggedOut()
    {
        await _loginPage.LogOut();
    }

    [When("user logs in using invalid username and password for Invalid")]
    public async Task WhenUserLogsInUsingInvalidUsernameAndPasswordForInvalid()
    {
        await _loginPage.UserLogin("Invalid");
    }

    [Then("error message should pop up")]
    public async Task ThenErrorMessageShouldPopUp()
    {
        bool isErrorValid = await _loginPage.IsErrorMessageDisplayedAsync("Invalid Login or Password.");
        if (!isErrorValid)
        {
            throw new Exception("Expected error message not displayed.");
        }
    }

}