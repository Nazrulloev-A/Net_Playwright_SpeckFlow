using DemoFramewrok.PageObjects;
using Reqnroll;
using System;
using WebOrders_PW.PageObjects;

namespace WebOrders_PW.StepDefinitions;

[Binding]
public class AddNewOrderStepDefinitions
{
    private readonly BasePage _basePage;
    private readonly AddNewOrder _addNewOrder;
   

    public AddNewOrderStepDefinitions(BasePage basePage,AddNewOrder newOrder)
    {
        _basePage = basePage;
        _addNewOrder = newOrder;
        
    }



    [Then("the user should click on this link button")]
    public async Task ThenTheUserShouldClickOnThisLinkButton()
    {
        await _addNewOrder.ClickOnLinkButton();
        await _addNewOrder.FillOrderInformation();
        await _addNewOrder.FillAddressInformation();
        await _addNewOrder.CreditCardInformation();
    }

}
