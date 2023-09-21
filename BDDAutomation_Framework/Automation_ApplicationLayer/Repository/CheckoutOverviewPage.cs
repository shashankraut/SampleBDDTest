using Automation_ApplicationLayer.BrowserUtilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer.Repository
{
    public class CheckoutOverviewPage
    {
        public CheckoutOverviewPage()
        {
        }

        #region Locators
        By _checkoutoverviewpagelabel = By.XPath("//span[@class='title' and text()='Checkout: Overview']");

        By _checkoutsummarypanel = By.Id("checkout_summary_container");

        By _finishbutton = By.Id("finish");

        By Cartitemwithexpectedname(string itemname)
        {
            return By.XPath($"//div[@class='inventory_item_name' and text()='{itemname}']/ancestor::div[@class='cart_item']");
        }
        #endregion

        #region Elements
        IWebElement PageLabel => BrowserUtils.FindElement(_checkoutoverviewpagelabel);

        IWebElement CheckoutSummaryPanel => BrowserUtils.FindElement(_checkoutsummarypanel);

        IWebElement FinishButton => BrowserUtils.FindElement(_finishbutton);
        #endregion

        #region Actions
        public void VerifyPageAvailability()
        {
            if (PageLabel is not null
                && CheckoutSummaryPanel is not null)
            {
                Console.WriteLine("Checkout: Overview page is available");
            }
            else
                throw new Exception("Checkout: Overview page not is available");
        }

        public void ClickOnFinishButton()
        {
            FinishButton?.Click(Enums.ClickType.WebElementClick);
        } 
        #endregion

    }
}
