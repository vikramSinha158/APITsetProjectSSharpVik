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

namespace PxTransformAutomation.StepDefinition
{
    [Binding]
    class TestDemoStepDef
    {
        private Settings _settings;
        public TestDemoStepDef(Settings settings) => _settings = settings;
        string facValue = string.Empty;

    

        [Given(@"User perform GET  operation for ""(.*)""")]
        public void GivenUserPerformGETOperationFor(string url)
        {          
            _settings.Request = _settings.lib.GetRequest(url, Method.GET);
        }

        [Given(@"User perform operation to get ""(.*)"" and ""(.*)""")]
        public void GivenUserPerformOperationToGetAnd(string facilitycode, string EncounterId)
        {

            _settings.ParameterList.Add("facilityCode", facilitycode);
            _settings.ParameterList.Add("EncounterId", EncounterId);

            _settings.Request.RequestFormat = DataFormat.Json;
    
            _settings.lib.AddPathParameter(_settings.Request, _settings.ParameterList);
            var response = _settings.lib.ExecuteAsyncRequest<RootObject>(_settings.RestClient,_settings.Request).GetAwaiter().GetResult();         
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
           
        }



    }
}
