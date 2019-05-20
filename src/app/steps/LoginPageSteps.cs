using OpenQA.Selenium;
using SpecflowDemo.src.app.pages;
using SpecflowDemo.src.config;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowDemo.src.app.steps
{
    [Binding]
    class LoginPageSteps
    {
        private readonly LoginPage lpage;
        public LoginPageSteps(WebDriver pageDriver)
        {
            lpage = new LoginPage(pageDriver);
        }

        [Given(@"user navigate to the target login page")]
        public void GivenUserNavigateToTheTargetLoginPage()
        {
            lpage.navigateToLoginPage();
        }

        [Given(@"user enter ""(.*)"" and ""(.*)""")]
        public void GivenUserEnterAnd(string username, string password)
        {
            lpage.setUserName(username);
            lpage.setPassword(password);
        }

        [When(@"user click the login button")]
        public void WhenUserClickTheLoginButton()
        {
            lpage.clickLoginBtn();
        }

        [Then(@"user should see the login success message")]
        public void ThenUserShouldSeeTheLoginSuccessMessage()
        {
            lpage.checkLoginSuccess();
        }


    }
}
