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
    }
}
