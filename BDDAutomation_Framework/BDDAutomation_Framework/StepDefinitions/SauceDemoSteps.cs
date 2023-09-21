using System;
using TechTalk.SpecFlow;
using Automation_ApplicationLayer;
using Automation_ApplicationLayer.BrowserUtilities;
using Automation_ApplicationLayer.Repository;
using System.Collections.Generic;

namespace BDDAutomation_Test.StepDefinitions
{
    [Binding]
    public class SauceDemoSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public SauceDemoSteps(ScenarioContext scenariocontext)
        {
            _scenarioContext = scenariocontext;
        }

        [Given(@"I launch the browser")]
        public void GivenILaunchTheBrowser()
        {
            BrowserUtils.InstantiateBrowser();
        }

        [Given(@"I navigate to SauceDemo")]
        public void GivenINavigateToSauceDemo()
        {
            BrowserUtils.NavigateToUrl(ConfigReader.SauceApplicationUrl);
        }

        [Given(@"I enter username as '([^']*)'")]
        public void GivenIEnterUsernameAs(string username)
        {
            Page.LoginPage.FillUsernameInput(username);
        }

        [Given(@"I enter password as '([^']*)'")]
        public void GivenIEnterPasswordAs(string password)
        {
            Page.LoginPage.FillPasswordInput(password);
        }

        [When(@"I click on login button")]
        public void WhenIClickOnLoginButton()
        {
            Page.LoginPage.ClickLoginButton();
        }

        [Then(@"Verify inventory page shall be available")]
        public void ThenVerifyInventoryPageShallBeAvailable()
        {
            Page.InventoryPage.VerifyPageAvailability();
        }

        [When(@"I add first item from list to cart and a make note of price")]
        public void WhenIAddFirstItemFromListToCartAndAMakeNoteOfPrice()
        {
            Page.InventoryPage.AddFirstItemFromListToCart(out string itemName, out double itemPrice);
            if (_scenarioContext.TryAdd("itemname", itemName) is false ||
            _scenarioContext.TryAdd("itemprice", itemPrice) is false)
            {
                throw new Exception("Failed to add item price and name to scenario context");
            }
            else
                Console.WriteLine("Item price and name are added to scenario context");
        }

        [When(@"I click on cart option")]
        public void WhenIClickOnCartOption()
        {
            Page.InventoryPage.ClickOnShoppingCartLink();
        }

        [Then(@"Verify cart page is available")]
        public void ThenVerifyCartPageIsAvailable()
        {
            Page.ShoppingCartPage.VeriryCartPageAvailability();
        }

        [Then(@"Verify shopping cart shall be available with details as expected")]
        public void ThenVerifyShoppingCartShallBeAvailable()
        {
            if (_scenarioContext.TryGetValue("itemname", out string itemName) is false ||
                _scenarioContext.TryGetValue("itemprice", out double itemPrice) is false)
            {
                throw new Exception("Item price and name details are not found from scenario context");
            }
            Page.ShoppingCartPage.VerifyExpectedItemAvailability(itemName, itemPrice);
        }

        [When(@"I click on checkout")]
        public void WhenIClickOnCheckout()
        {
            Page.ShoppingCartPage.ClickOnCheckoutButton();
        }

        [When(@"I fill details as firstname as '([^']*)' lastname as '([^']*)' and zipcode as '([^']*)'")]
        public void WhenIFillDetailsAsFirstnameAsLastnameAsAndZipcodeAs(string firstname, string lastname, string zipcode)
        {
            Page.CheckoutStepOnePage.VerifyPageAvailability();
            Page.CheckoutStepOnePage.FillCustomerInformation(firstname, lastname, zipcode);
        }

        [When(@"I click on continue")]
        public void WhenIClickOnContinue()
        {
            Page.CheckoutStepOnePage.ClickOnContinueButton();
        }

        [Then(@"I click Finish")]
        public void ThenIClickFinish()
        {
            Page.CheckoutStepTwoPage.VerifyPageAvailability();
            Page.CheckoutStepTwoPage.ClickOnFinishButton();
        }


    }
}
