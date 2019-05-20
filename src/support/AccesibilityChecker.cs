using Globant.Selenium.Axe;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SpecflowDemo.src.support
{
    class AccesibilityChecker
    {


        public void PerformAccessbilityAudit(IWebDriver _driver)
        {
            IWebDriver webDriver = new FirefoxDriver();
            AxeResult results = webDriver.Analyze();
        }
    }
}
