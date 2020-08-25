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
using PxTransform.Auto.Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PxTransformAutomation.Reports;

namespace PxTransformAutomation.Hooks
{
    
  


    [Binding]
    class Hooks
    {
        //Global Variable for Extend report

        #region Fields   
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
       
        private static AventStack.ExtentReports.ExtentReports extent;
        private readonly ScenarioContext _scenarioContext;
        private Settings _settings;
        #endregion

        #region Ctor
        public Hooks(Settings settings,ScenarioContext scenarioContext)
        {
            _settings = settings;
            _scenarioContext = scenarioContext;
            _settings.config = _settings.Util.TranViewDataServiceUrl;
           
        }
        #endregion

        #region Methods
        /// <summary>
        /// Used differnt Hooks for initialization of test case and Report
        /// </summary>
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
          
            extent = ReportConfiguration.InitReport(extent);

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

        }

        [AfterStep]
        public void InsertReportingSteps()
        {
         
            _settings.Report.InsertStepsInReport(_scenarioContext,scenario);
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

        #endregion
    }
}
