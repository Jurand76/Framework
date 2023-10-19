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
    public class MainPage
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        private readonly string pageUrl = "https://cloud.google.com/";

        public MainPage()
        {
            PageFactory.InitElements(driver, this);
        }

        public MainPage openPage()
        {
            driver.Navigate().GoToUrl(pageUrl);
            return this;
        }
    }
}
