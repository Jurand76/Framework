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
    public class YopmailPage
    {
        readonly IWebDriver driver = DriverSingleton.getDriver();
        readonly ConfigModel configModel = ConfigModel.GetConfiguration();
        readonly string pageUrl;

        public YopmailPage()
        {
            pageUrl = configModel.Url_mail_service;
            PageFactory.InitElements(driver, this);
        }

        public YopmailPage openPage()
        {
            driver.Navigate().GoToUrl(pageUrl);
            return this;
        }
    }
}
