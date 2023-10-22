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
using NLog;

namespace Framework.service
{
    public class YopMailPageActions
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        private WebDriverWait wait;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

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
            logger.Info("Closing popup at mail service page.");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(cookiesAcceptButton));
            cookiesAcceptButton.Click();
        }

        public void GenerateMail()
        {
            logger.Info("Generating temporary email address");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(generateMailButton));
            generateMailButton.Click();
        }

        public string GetGeneratedMail()
        {
            string emailUsername = driver.FindElement(By.CssSelector("#geny .genytxt")).Text;
            string emailDomain = driver.FindElement(By.CssSelector("#geny .genytxt:nth-child(2)")).Text;
            string fullEmail = emailUsername + "@" + emailDomain;
            logger.Info("Reading temporary email address: " + fullEmail);

            return fullEmail;
        }

        public string CheckEmailBoxAndReceiveAmount()
        {
            logger.Info("Checking mail box for mail from google calculator");
            IWebElement checkEmailButton = driver.FindElement(By.CssSelector("button[onclick='egengo();']"));
            checkEmailButton.Click();
            driver.SwitchTo().Frame("ifmail");
            IWebElement priceElement = driver.FindElement(By.XPath("//tr/td/h3[contains(text(), 'USD')]"));
            string priceText = priceElement.Text;
            
            return priceText;
        }

        public void CreateNewTab()
        {
            logger.Info("Creating new tab in web browser");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public void SwitchToPreviousTab()
        {
            logger.Info("Switching to previous tab in web browser");
            string currentHandle = driver.CurrentWindowHandle;
            int currentTabIndex = driver.WindowHandles.ToList().IndexOf(currentHandle);
            int previousTabIndex = currentTabIndex - 1;
            driver.SwitchTo().Window(driver.WindowHandles[previousTabIndex]);
        }

        public void SwitchToNextTab()
        {
            logger.Info("Switching to next tab in web browser");
            string currentHandle = driver.CurrentWindowHandle;
            int currentTabIndex = driver.WindowHandles.ToList().IndexOf(currentHandle);
            int previousTabIndex = currentTabIndex + 1;
            driver.SwitchTo().Window(driver.WindowHandles[previousTabIndex]);
        }
    }
}
