using Automation_ApplicationLayer.BrowserUtilities;
using Automation_ApplicationLayer.Enums;
using Automation_ApplicationLayer.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Automation_ApplicationLayer.Repository
{
    public class LoginPage
    {
        public LoginPage()
        {
            PageFactory.InitElements(BrowserUtils.WebDriver, this);
        }


        #region Locators
        By _usernametextbox = By.Id("user-name");
        By _passwordtextbox = By.Id("password");
        By _loginbutton = By.Id("login-button");
        #endregion

        #region Elements
        IWebElement? UserNameTextbox => BrowserUtils.FindElement(_usernametextbox);

        IWebElement? PasswordTextbox => BrowserUtils.FindElement(_passwordtextbox);

        IWebElement? LoginButton => BrowserUtils.FindElement(_loginbutton);
        #endregion

        #region Actions
        public void FillUsernameInput(string username)
        {
            UserNameTextbox?.SendKeys(username);
        }

        public void FillPasswordInput(string password)
        {
            PasswordTextbox?.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            LoginButton?.Click(ClickType.WebElementClick);
        }
        #endregion
    }
}
