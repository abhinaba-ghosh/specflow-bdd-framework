using BoDi;
using OpenQA.Selenium;
using System.Reflection;
using SpecflowDemo.src.config;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using System;
using System.IO;
using System.Configuration;
using Globant.Selenium.Axe;
using NUnit.Framework;

namespace SpecflowDemo.src.config
{
    [Binding]
    class Hooks
    {
        //Global Variable for Extend report
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        public static string projectPath = ConfigurationManager.AppSettings["reportPath"];
        private static WebDriver _driver;

        public Hooks(WebDriver driver)
        {
            _driver = driver;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Initialize Extent report before test starts
            Console.WriteLine("project path:" + projectPath);
            var htmlReporter = new ExtentHtmlReporter(@projectPath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //Attach report to reporter
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {

            switch (ScenarioStepContext.Current.StepInfo.StepDefinitionType)
            {
                case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                    scenario.StepDefinitionGiven(); // extension method
                    break;

                case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                    scenario.StepDefinitionThen(); // extension method
                    break;

                case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                    scenario.StepDefinitionWhen(); // extension method
                    break;
            }

        }


        [BeforeScenario]
        public void Initialize()
        {

            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterScenario (Order =2)]
        public void CleanUp()
        {
            
            _driver.Current.Quit();
        }

        [AfterScenario(Order =1)]
        public void accesibilityTest()
        {
            AxeResult result = _driver.Current.Analyze();
            Assert.True(result.Violations.Length == 0, "There are accessibility violations. Please check log file");
        }

    }
}
