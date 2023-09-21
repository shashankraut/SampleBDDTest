using Automation_ApplicationLayer.BrowserUtilities;
using Automation_ApplicationLayer.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer.Repository
{
    public class ShoppingCartPage
    {
        public ShoppingCartPage()
        {
            PageFactory.InitElements(BrowserUtils.WebDriver, this);
        }

        #region Locators
        By _cartpagelabel = By.XPath("//span[@class='title' and text()='Your Cart']");
        By _cartfooter = By.ClassName("cart_footer");
        By _cartlist = By.ClassName("cart_list");
        By Cartitemwithexpectedname(string itemname)
        {
            return By.XPath($"//div[@class='inventory_item_name' and text()='{itemname}']/ancestor::div[@class='cart_item']");
        }

        By _itemprice = By.ClassName("inventory_item_price");
        By _checkoutbutton = By.Id("checkout");
        #endregion

        #region Elements
        IWebElement? CartPageLabel => BrowserUtils.FindElement(_cartpagelabel, 60);

        IWebElement? CartList => BrowserUtils.FindElement(_cartlist);

        IWebElement CartFooter => BrowserUtils.FindElement(_cartfooter);

        IWebElement CheckoutButton => BrowserUtils.FindElement(_checkoutbutton);
        #endregion

        #region Actions
        public void VeriryCartPageAvailability()
        {
            if (CartPageLabel is not null
                && CartList is not null
                && CartFooter is not null)
            {
                Console.WriteLine("Cart Page is found available");
            }
            else
            {
                throw new Exception("Cart page is not found available");
            }
        }

        public void VerifyExpectedItemAvailability(string itemname, double itemprice)
        {
            var expectedCartItem = BrowserUtils.FindElement(Cartitemwithexpectedname(itemname))
                ?? throw new Exception($"Shopping cart not found as expected with name {itemname}");

            var itemPriceLabel = expectedCartItem.FindElement(_itemprice, 10);
            string? availableprice = itemPriceLabel?.Text;
            if (string.IsNullOrEmpty(availableprice))
                throw new Exception("Inventory price not found from shopping cart");
            var availableitemprice = Convert.ToDouble(availableprice.Replace("$", string.Empty).Trim());

            Assert.AreEqual(availableitemprice, itemprice,
                            message: $"Price found from cart is not same as expected. Found = {availableitemprice} and expected = {itemprice}");
        }

        public void ClickOnCheckoutButton()
        {
            CheckoutButton?.Click(ClickType.WebElementClick);
        }
        #endregion
    }
}
