using Xunit;
using PxTransformAutomation.Base;
using PxTransformAutomation.Model;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using PxTransformAutomation.Utilities;
using Microsoft.EntityFrameworkCore;
using PxTransform.Auto.Data;
using PxTransform.Auto.Data.Data;
using PxTransform.Auto.Data.Domain.Accretive;

using System.Linq;
using PxTransform.Auto.Data.Domain.Tran;
using PxTransformAutomation.DataService;

namespace PxTransformAutomation.StepDefinition
{
    [Binding]
    class TestDemoStepDef
    {
       
        private Settings _settings;
        private readonly TranContext tranContext;
        private DataCollector _dataCollector;
        private readonly ScenarioContext _scenarioContext;
        string facValue = string.Empty;

        public TestDemoStepDef(Settings settings, DataCollector dataCollector, ScenarioContext scenarioContext)
        {
            _settings = settings;
            _dataCollector = dataCollector;
            _scenarioContext = scenarioContext;
            tranContext = _dataCollector.GetLocationInstance().GetTranContext(_settings.Util.TranViewDataServiceUrl["ConnectionStrings:TranFacilityCodeBCOR"]);
        } 
        

        [Given(@"User perform GET  operation for ""(.*)""")]
        public void GivenUserPerformGETOperationFor(string url)
        {

            //var regdata  = _dataCollector.GetRegisterInstance().GetAuthEligibleAccounts(tranContext);

            //foreach (var item in regdata)
            //{
            //    string dddff = item.ID.ToString();
            //    string fffdfd = item.PatientType.ToString();
            //    string kkk = item.EncounterID.ToString();
            //} 
            string dddddd = _settings.Util.GetTestData("FacilityCode");
            _settings.Request = _settings.lib.GetRequest(url, Method.GET);
        }

        [Given(@"user send the path parmeter as ""(.*)"" and ""(.*)""")]
        public void GivenUserSendThePathParmeterAsAnd(string facilitycode, string EncounterId)
        {

            _settings.ParameterList.Add("facilityCode", facilitycode);
            _settings.ParameterList.Add("EncounterId", EncounterId);
            _settings.lib.AddPathParameter(_settings.Request, _settings.ParameterList);
        }

        [Given(@"user the get the response for the request")]
        public void GivenUserTheGetTheResponseForTheRequest()
        {
            _settings.Request.RequestFormat = DataFormat.Json;
            var response = _settings.lib.ExecuteAsyncRequest<ServiceActivity>(_settings.RestClient, _settings.Request).GetAwaiter().GetResult();
            facValue = response.Data.Visit.FacilityCode;
        }



        [Then(@"User should see the ""(.*)"" name as ""(.*)""")]
        public void ThenUserShouldSeeTheNameAs(string p0, string p1)
        {
            Assert.Equal("BOMC", facValue);     
        }

        [Given(@"User perform POST  operation for ""(.*)""")]
        public void GivenUserPerformPOSTOperationFor(string url)
        {
            _settings.Request = _settings.lib.GetRequest(url, Method.POST);
        }

        [Given(@"User send the path parameter ""(.*)"" and ""(.*)""")]
        public void GivenUserSendThePathParameterAnd(string facilitycode, string EncounterId)
        {
            _settings.ParameterList.Add("facilityCode", facilitycode);
            _settings.ParameterList.Add("EncounterId", EncounterId);
            _settings.lib.AddPathParameter(_settings.Request, _settings.ParameterList);

        }

        [Given(@"User send ""(.*)"" as a body for POST request")]
        public void GivenUserSendAsABodyForPOSTRequest(string TextData)
        {
            string filepath = _settings.Util.GetFolderPath("TestData") + TextData + ".json";
            string postData=_settings.Util.LoadJson(filepath);
            _settings.lib.AddPostRequestBody(_settings.Request, postData);
        }

        [Then(@"User should receive ""(.*)"" as a Status code")]
        public void ThenUserShouldReceiveAsAStatusCode(int p0)
        {
        
            _settings.Response=_settings.lib.ExecuteAsyncRequest<TextInfo>(_settings.RestClient, _settings.Request).GetAwaiter().GetResult();
            Dictionary<string, string> responseData = _settings.lib.DeserializeResponse(_settings.Response);
            Assert.Equal("201", responseData["Status"]);
        }

        [Then(@"User should see theFacilityCode ""(.*)""")]
        public void ThenUserShouldSeeTheFacilityCode(string p0)
        {
            Assert.Equal("BOM", facValue);
        }


    }
}
