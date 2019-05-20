using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SpecflowDemo.src.config
{
    public class WebDriver
    {
        private IWebDriver _currentWebDriver;

        public IWebDriver Current
        {
            get
            {
                if (_currentWebDriver != null)
                    return _currentWebDriver;

                switch (BrowserConfig)
                {
                    case "IE":
                        _currentWebDriver = new InternetExplorerDriver(new InternetExplorerOptions() { IgnoreZoomLevel = true }) { };
                        break;
                    case "Chrome":
                        _currentWebDriver = new ChromeDriver(@driverPath) {};
                        break;
                    case "Firefox":
                        _currentWebDriver = new FirefoxDriver() { };
                        break;
                    default:
                        throw new NotSupportedException($"{BrowserConfig} is not a supported browser");
                }

                return _currentWebDriver;
            }
        }

        private WebDriverWait _wait;
        public WebDriverWait Wait
        {
            get
            {
                if (_wait == null)
                {
                    this._wait = new WebDriverWait(Current, TimeSpan.FromSeconds(10));
                }
                return _wait;
            }
        }

        protected string BrowserConfig => ConfigurationManager.AppSettings["browser"];
        protected string SeleniumBaseUrl => ConfigurationManager.AppSettings["seleniumBaseUrl"];

        protected string driverPath => ConfigurationManager.AppSettings["driverPath"];

        public void Quit()
        {
            _currentWebDriver?.Quit();
        }
    }
}