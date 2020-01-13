using NUnit.Framework;
using PxTransformAutomation.Base;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
            //IRestRequest restRequest;
            //this._settings.Request = restRequest;
            _settings.Request = new RestRequest("r1/rcm/activity/Service/{facilityCode}/v1.0/Visit/{EncounterId}", Method.GET);
           
            

        }

        [Given(@"User perform operation to get ""(.*)"" and ""(.*)""")]
        public void GivenUserPerformOperationToGetAnd(string facilitycode, string EncounterId)
        {
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Request.AddUrlSegment("facilityCode", facilitycode);
            _settings.Request.AddUrlSegment("EncounterId", EncounterId);

            _settings.Response = _settings.RestClient.Execute<Visit1>(_settings.Request);
           
            var deserialize = new JsonDeserializer();
            var visitOutPut = deserialize.Deserialize<Visit1>(_settings.Response);
            facValue = visitOutPut.Visit.FacilityCode;

        }

        [Then(@"User should see the ""(.*)"" name as ""(.*)""")]
        public void ThenUserShouldSeeTheNameAs(string p0, string p1)
        {
            Assert.That(facValue, Is.EqualTo("BOMC"), "Dat not matched");
        }


    }
}
