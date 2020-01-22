using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using System.Configuration;
using PxTransformAutomation.Base;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PxTransformAutomation.Hooks
{
    
  


    [Binding]
    class Hooks
    {
        //Global Variable for Extend report

            
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extent;
        private readonly ScenarioContext _scenarioContext;

        private Settings _settings;
        //IConfigurationRoot config;
        public Hooks(Settings settings,ScenarioContext scenarioContext)
        {
            _settings = settings;
            _scenarioContext = scenarioContext;
            _settings.config = _settings.Util.TranViewDataServiceUrl;
        }

        [BeforeScenario]
        public void TestSetup()
        {
           string baseUrl= _settings.config["ConnectionStrings:baseUrl"];
            _settings.BaseUrl = new Uri(baseUrl);
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
         
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string file = "ExtentReport.html";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
            //Initialize Extent report before test starts
            var htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //Attach report to reporter
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);

            
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            Thread.Sleep(2000);
            //Flush report once test completes
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            Console.WriteLine(featureName);

        }

        [AfterStep]
        public void InsertReportingSteps()
        {

            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
            }
        }

        [BeforeScenario]
        public void Initialize()
        {
        //Create dynamic scenario name
        scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }
    }
}
