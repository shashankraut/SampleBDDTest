using AngleSharp.Dom;
using Automation_ApplicationLayer.Enums;
using Automation_ApplicationLayer.Reports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V114.Storage;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing.Text;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Automation_ApplicationLayer.BrowserUtilities
{
    public static class BrowserUtils
    {
        private static IWebDriver webDriver;
        public static IWebDriver WebDriver
        {
            get
            {
                return webDriver;
            }
        }

        public static void InstantiateBrowser()
        {
            switch (ConfigReader.Browser.ToLower())
            {
                case "chrome":
                    string driverPath = new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    FileInfo chromeDriverFileInfo = new FileInfo(driverPath);

                    var chromeOptions = new ChromeOptions();
                    webDriver = new ChromeDriver(chromeDriverFileInfo?.Directory?.FullName, chromeOptions, TimeSpan.FromMinutes(3));
                    webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                    webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(3);
                    webDriver.Manage().Window.Maximize();

                    break;

                default:
                    throw new ArgumentException("Invalid choice for browser");
            }
        }

        public static void NavigateToUrl(string? url)
        {
            webDriver?.Navigate().GoToUrl(url);
        }

        public static IWebElement FindElement(By locator, int timeout = 30, string exceptionMessage = "", bool throwException = true, bool takeScreenshot = true)
        {
            IWebElement? webElement = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
                webElement = wait.Until(ExpectedConditions.ElementExists(locator));
                Reporting.Log(LogStatus.Info, $"Control with locator {locator} is found");
            }
            catch (Exception ex)
            {
                exceptionMessage = !string.IsNullOrEmpty(exceptionMessage) ? exceptionMessage : $"Control with locator {locator} is not found with exception : {ex.Message}";
                var logStatus = throwException ? LogStatus.Error : LogStatus.Info;
                Reporting.Log(logStatus, exceptionMessage, takeScreenshot, throwException);
            }
            return webElement;

        }

        public static IWebElement FindElement(this IWebElement element, By locator, int timeout = 30, string exceptionMessage = "", bool throwException = true, bool takeScreenshot = true)
        {
            IWebElement? webElement = null;
            try
            {

                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
                webElement = wait.Until(d => element.FindElement(locator));
            }
            catch (Exception ex)
            {
                exceptionMessage = !string.IsNullOrEmpty(exceptionMessage) ? exceptionMessage : $"Control with locator {locator} is not found with exception : {ex.Message}";
                var logStatus = throwException ? LogStatus.Error : LogStatus.Info;
                Reporting.Log(logStatus, exceptionMessage, takeScreenshot, throwException);
            }
            return webElement;
        }

        public static void Click(this IWebElement element, ClickType clickType)
        {
            switch (clickType)
            {
                case ClickType.WebElementClick:
                    element.Click();
                    break;

                case ClickType.SendKeysEnter:
                    element.SendKeys(Keys.Enter);
                    break;

                case ClickType.JSClick:
                    {
                        IJavaScriptExecutor? jsExecutor = webDriver as IJavaScriptExecutor;
                        jsExecutor?.ExecuteScript("arguments[0].click();", element);
                    }
                    break;

                case ClickType.MouseActions:
                    {
                        Actions actions = new Actions(webDriver);
                        actions.MoveToElement(element).Click().Build().Perform();
                    }
                    break;

                case ClickType.ContextClick:
                    {
                        Actions actions = new Actions(webDriver);
                        actions.ContextClick(element).Perform();
                    }
                    break;
            }
        }

        public static Screenshot? GetScreenshot()
        {
            if (webDriver is null) return null;
            return ((ITakesScreenshot)webDriver).GetScreenshot();
        }

        public static void Quit()
        {
            if (webDriver is not null) 
            webDriver.Quit();
        }
    }
}
