using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;
using PxTransformAutomation.Base;
using PxTransformAutomation.Model;
using RestSharp;
using PxTransform.Auto.Data.Data;
using Newtonsoft.Json.Linq;
using PxTransform.Auto.Data.Domain.Tran;
using System.Collections;

namespace PxTransformAutomation.StepDefinition.AuthSchedulerStep
{

    [Binding]
    class AuthSchedulerStepDef
    {
        private Settings _settings;
        private readonly TranContext _tranContext;
        private readonly AccretiveContext _accretiveContext;
        private DataCollector _dataCollector;
        private int getStatusCode;
        private List<int> actualResistrationIDs;

        public AuthSchedulerStepDef(Settings settings, DataCollector dataCollector)
        {
            _settings = settings;
            _dataCollector = dataCollector;
            _tranContext = _dataCollector.GetLocationInstance().GetTranContext(_settings.Util.TranViewDataServiceUrl["ConnectionStrings:TranFacilityCodeBCOR"]);
            _accretiveContext = _dataCollector.GetLocationInstance().GetAccretiveContext();
    }
        [Given(@"User has GET request AuthAPI with mindaysout and maxdaysout parameters")]
        public void GivenUserHasGETRequestAuthAPIWithMindaysoutAndMaxdaysoutParameters()
        {
            _settings.Request = _settings.lib.GetRequest("api/authorization?mindaysout=0&maxdaysout=5", Method.GET);
        }

        [Given(@"Add Header As facilityCode With its value")]
        public void GivenAddHeaderAsFacilityCodeWithItsValue()
        {
            _settings.Request.AddHeader("facilityCode", "BCOR");
        }

        [When(@"User executes the GET request")]
        public void WhenUserExecutesTheGETRequest()
        {
            _settings.Request.RequestFormat = DataFormat.Json;

            var res1 = _settings.lib.ExecuteAsyncRequest<Model.Authorization>(_settings.RestClient, _settings.Request).GetAwaiter().GetResult();
            getStatusCode = (int)res1.StatusCode;
            actualResistrationIDs = res1.Data.recordKeys;
            actualResistrationIDs.Sort();
        }

        [Then(@"user should get success status response code")]
        public void ThenUserShouldGetSuccessStatusResponseCode()
        {

            Assert.True(getStatusCode.Equals(200), "Expected Status Code is:- " + 200 + " But Found:- " + getStatusCode);
        }

        [Then(@"user should get eligible accounts whose patienttype is not E in the response")]
        public void ThenUserShouldGetEligibleAccountsWhosePatienttypeIsNotEInTheResponse()
        {
            var authE = _dataCollector.GetRegisterInstance().GetAuthEligibleAccounts(_tranContext, _accretiveContext);

            List<int> regisID = new List<int>();
            foreach (var item in authE)
            {
                regisID.Add(item.RegistrationID);
            }
            regisID.Sort();
            Assert.True(actualResistrationIDs.Equals(regisID), "Eexpected Data " + regisID.Count + " But found:- " + actualResistrationIDs.Count);
        }


        [Given(@"user hits the AuthUri  with mindaysout and maxdaysout parameters")]
        public void GivenUserHitsTheAuthUriWithMindaysoutAndMaxdaysoutParameters()
        {
            //var regdata = _dataCollector.GetRegisterInstance().GetAuthEligibleAccounts(_tranContext);
            _settings.Request = _settings.lib.GetRequest("api/authorization?mindaysout=0&maxdaysout=5", Method.GET);
            
        }

       

    }
}
