using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using SpecflowDemo.src.config;
using System.Configuration;

namespace SpecflowDemo.src.app.pages
{
    class LoginPage
    {

        [FindsBy(How = How.Name, Using = "username")]
        public IWebElement userNameTxtField;

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement passwordTxtField;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"login\"]/button")]
        public IWebElement loginBtn;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'success')]")]
        public IWebElement loginSuccessMessage;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'error')]")]
        public IWebElement loginInvalidMessage;

        private readonly WebDriver _driver;
        public LoginPage(WebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver.Current, this);
        }

        public void navigateToLoginPage()
        {
            _driver.Current.Navigate().GoToUrl(ConfigurationManager.AppSettings["seleniumBaseUrl"]+"/login");
        }
        public void setUserName(string username)
        {
            userNameTxtField.SendKeys(username);
        }

        public void setPassword(string password)
        {
            passwordTxtField.SendKeys(password);
        }

        public void clickLoginBtn()
        {
            loginBtn.Click();
        }

        public void checkLoginSuccess()
        {
            NUnit.Framework.Assert.AreEqual(true,loginSuccessMessage.Displayed);
        }

        public void checkLoginErrorMessageDislayed()
        {
            NUnit.Framework.Assert.AreEqual(true, loginInvalidMessage.Displayed);
        }

    }
}
