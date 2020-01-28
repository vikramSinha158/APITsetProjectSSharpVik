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
using System.Reflection;

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

            var folderName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);           
            var path = Path.Combine(folderName.Substring(0, folderName.LastIndexOf("\\bin"))+"\\Reports", file);     
            var htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);




        }

        [AfterTestRun]
        public static void TearDownReport()
        {
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
            

            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(_scenarioContext, null);

            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null && TestResult.ToString()!= "StepDefinitionPending")
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


            //Pending Status
            if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");

            }
        }

        [BeforeScenario]
        public void Initialize()
        {
        //Create dynamic scenario name
        scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        private string GetCongigData()
        {
            return _settings.config["ConnectionStrings:ReportName"];
        }
    }
}
