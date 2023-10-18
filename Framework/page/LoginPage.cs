using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;


namespace Framework.page
{
    public class LoginPage
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        private readonly string pageUrl = "http://poczta.interia.pl";

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement? emailField;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement? passwordField;

        [FindsBy(How = How.ClassName, Using = "btn")]
        private IWebElement? loginButtonField;

        [FindsBy(How = How.ClassName, Using = "account-info__logout")]
        private IWebElement? logoutButtonField;

        public LoginPage()
        {
            PageFactory.InitElements(driver, this);
        }

        public LoginPage openPage()
        {
            driver.Navigate().GoToUrl(pageUrl);
            return this;
        }

        public void rodoPopupClose()
        {
            By rodoPopupContentLocator = By.ClassName("rodo-popup-content");
            By rodoPopupAgreeLocator = By.ClassName("rodo-popup-agree");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(rodoPopupContentLocator));
                driver.FindElement(rodoPopupAgreeLocator).Click();
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(rodoPopupContentLocator));
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"Rodo popup - timeout exception: {ex.Message}");
            }
        }

        public void loginUser(string userName, string userPassword)
        {
            emailField.SendKeys(userName);
            passwordField.SendKeys(userPassword);
            loginButtonField.Click();
        }

    }
}
