using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.driver;
using OpenQA.Selenium;

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
    }
}
