using Automation_ApplicationLayer.BrowserUtilities;
using Automation_ApplicationLayer.Enums;
using Automation_ApplicationLayer.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer.Repository
{
    public class InventoryPage
    {
        public InventoryPage()
        {
            PageFactory.InitElements(BrowserUtils.WebDriver, this);
        }

        #region Locators
        By _swagpagelabel = By.XPath("//div[@class='app_logo' and  text()='Swag Labs']");
        By _productstitle = By.XPath("//span[@class='title' and  text()='Products']");
        By _inventorycontainer = By.Id("inventory_container");
        By _firstinventoryitem = By.XPath("//div[@class='inventory_item'][1]");
        By _inventoryitemname = By.XPath("//div[@class='inventory_item_name']");
        By _inventoryprice = By.XPath("//div[@class='inventory_item_price']");
        By _additemtocartbutton = By.Id("add-to-cart-sauce-labs-backpack");
        By _shoppingcartlink = By.ClassName("shopping_cart_link");
        #endregion

        #region Elements
        IWebElement? SwagPageLabel => BrowserUtils.FindElement(_swagpagelabel);

        IWebElement? ProductsTitle => BrowserUtils.FindElement(_productstitle);

        IWebElement? InventoryContainer => BrowserUtils.FindElement(_inventorycontainer);

        IWebElement? InventoryFirstItem => BrowserUtils.FindElement(_firstinventoryitem);

        IWebElement? ShoppingCartLink => BrowserUtils.FindElement(_shoppingcartlink);
        #endregion

        #region Actions
        public void VerifyPageAvailability()
        {
            if (SwagPageLabel != null
                && ProductsTitle != null
                && InventoryContainer != null)
            {
                Console.WriteLine("Inventory page is found available");
            }
            else
            {
                throw new Exception("Inventory page is not available");
            }
        }

        public void AddFirstItemFromListToCart(out string itemname, out double itemprice)
        {
            if (InventoryFirstItem == null)
            {
                throw new Exception("First item from ther inventory ");
            }
            else
            {
                IWebElement inventoryName = InventoryFirstItem.FindElement(_inventoryitemname, 10);
                string? name = inventoryName?.Text;
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Inventory name not found from first cart");
                itemname = name;

                IWebElement inventoryPrice = InventoryFirstItem.FindElement(_inventoryprice, 10);
                string? price = inventoryPrice?.Text;
                if (string.IsNullOrEmpty(price))
                    throw new Exception("Inventory price not found from first cart");
                itemprice = Convert.ToDouble(price.Replace("$", string.Empty).Trim());

                IWebElement addToCartButton = InventoryFirstItem.FindElement(_additemtocartbutton, 10);
                addToCartButton.Click(ClickType.WebElementClick);
            }
        }

        public void ClickOnShoppingCartLink()
        {
            ShoppingCartLink?.Click(ClickType.WebElementClick);
        }
        #endregion
    }
}
