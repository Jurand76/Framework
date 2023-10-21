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
using Framework.model;
using Framework.page;

namespace Framework.service
{
    public class YopMailPageActions
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        private WebDriverWait wait;

        [FindsBy(How = How.Id, Using = "accept")]
        private IWebElement cookiesAcceptButton;

        [FindsBy(How = How.CssSelector, Using = "a[href='email-generator']")]
        private IWebElement generateMailButton;

        public YopMailPageActions()
        {
            PageFactory.InitElements(driver, this);
        }

        public void CloseCookiesPopup()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(cookiesAcceptButton));
            cookiesAcceptButton.Click();
        }

        public void GenerateMail()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(generateMailButton));
            generateMailButton.Click();
        }
    }
}
