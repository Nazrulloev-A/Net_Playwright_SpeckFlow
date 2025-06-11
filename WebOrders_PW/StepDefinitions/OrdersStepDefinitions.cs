using Reqnroll;
using WebOrders_PW.PageObjects;
using WebOrders_PW.PageObjects.OrderPage;

namespace WebOrders_PW.StepDefinitions;


[Binding]
public class OrdersStepDefinitions
{

    private readonly BasePage _basePage;
    private readonly LoginPage _loginPage;
    private readonly OrderPage _orderPage;



    public  OrdersStepDefinitions(BasePage basePage)
    {
        _basePage = basePage;
        _loginPage = new LoginPage(_basePage.Page, _basePage.Config);
        _orderPage = new OrderPage(_basePage.Page, _basePage.Config);
    }



    [Given("I am on the Web Orders page")]
    public async Task GivenIAmOnTheWebOrdersPage()
    {
        await _orderPage.ValidateTitle();
    }
    
    [Then("I should see the orders grid with at least one record")]
    public async Task ThenIShouldSeeTheOrdersGridWithAtLeastOneRecord()
    {
        
        await _orderPage.ValidateListOfAllOrders();
    }
    
    [When("I select all orders")]
    public async Task WhenISelectAllOrders()
    {
        await _orderPage.ClickCheckAllBtn();
    }
    
    [When("I click the Delete Selected button")]
    public async Task WhenIClickTheDeleteSelectedButton()
    {
        
        await _orderPage.ClickOnDeleteBtn();
       
    }
    
    [Then("the grid should be empty or show a deletion success message")]
    public async Task ThenTheGridShouldBeEmptyOrShowADeletionSuccessMessage()
    {
        await _orderPage.EmptyListMessageValidation();
    }
    
    [When("I navigate to the Order page")]
    public async Task WhenINavigateToTheOrderPage()
    {
        await _orderPage.ClickOnLink();
    }
    
    [When("I fill in and submit a new order")]
    public async Task WhenIFillInAndSubmitANewOrder()
    {
        
    }
    
    // [Then("the new order should appear in the orders grid")]
    // public void ThenTheNewOrderShouldAppearInTheOrdersGrid()
    // {
    //     ScenarioContext.StepIsPending();
    // }


}