using Automation_ApplicationLayer.BrowserUtilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer.Repository
{
    public class CheckoutYourInformationPage
    {
        public CheckoutYourInformationPage()
        {

        }

        #region Locators
        By _pagelabel = By.XPath("//span[@class='title' and text()='Checkout: Your Information']");

        By _checkoutInfo = By.ClassName("checkout_info");

        By _checkoutbuttons = By.ClassName("checkout_buttons");

        By _firstnametextbox = By.Id("first-name");

        By _lastnametextbox = By.Id("last-name");

        By _zipcodetextbox = By.Id("postal-code");

        By _continuebutton = By.Id("continue");
        #endregion


        #region Elements
        IWebElement PageLabel => BrowserUtils.FindElement(_pagelabel);

        IWebElement CheckoutInfo => BrowserUtils.FindElement(_checkoutInfo);

        IWebElement CheckoutButtons => BrowserUtils.FindElement(_checkoutbuttons);

        IWebElement FirstNameTextbox => BrowserUtils.FindElement(_firstnametextbox);

        IWebElement LastNameTextbox => BrowserUtils.FindElement(_lastnametextbox);

        IWebElement ZipcodeTextbox => BrowserUtils.FindElement(_zipcodetextbox);

        IWebElement ContinueButton => BrowserUtils.FindElement(_continuebutton);
        #endregion


        #region Actions
        public void VerifyPageAvailability()
        {
            if (PageLabel is not null
                && CheckoutInfo is not null
                && CheckoutButtons is not null)
            {
                Console.WriteLine("Checkout: Your Information page is available");
            }
            else
                throw new Exception("Checkout: Your Information page not is available");
        }

        public void FillCustomerInformation(string firstname, string lastname, string zipcode)
        {
            FirstNameTextbox?.SendKeys(firstname);
            LastNameTextbox?.SendKeys(lastname);
            ZipcodeTextbox?.SendKeys(zipcode);
        }

        public void ClickOnContinueButton()
        {
            ContinueButton?.Click();
        }
        #endregion

    }
}
