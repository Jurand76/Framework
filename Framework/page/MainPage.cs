using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.driver;
using Framework.model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;


namespace Framework.page
{
    public class MainPage
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        readonly ConfigModel configModel = ConfigModel.GetConfiguration();
        readonly string pageUrl;
        
        public MainPage()
        {
            pageUrl = configModel.Url_google_cloud;
            PageFactory.InitElements(driver, this);
        }

        public MainPage openPage()
        {
            driver.Navigate().GoToUrl(pageUrl);
            return this;
        }
    }
}
