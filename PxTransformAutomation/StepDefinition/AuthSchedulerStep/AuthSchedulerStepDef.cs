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
using System.Linq;

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
            
            _settings.Request = _settings.lib.GetRequest(_settings.Util.GetTestData("Authurl"), Method.GET);
            _settings.ParameterList.Add("mindaysout",_settings.Util.GetTestData("MinDaysOut"));
            _settings.ParameterList.Add("maxdaysout", _settings.Util.GetTestData("MaxDaysOut"));
            _settings.lib.AddQueryParameter(_settings.Request, _settings.ParameterList);

        }

        [Given(@"Add Header As facilityCode With its value")]
        public void GivenAddHeaderAsFacilityCodeWithItsValue()
        {
            _settings.Util.CleanDictionary(_settings.ParameterList);
            _settings.ParameterList.Add("facilityCode", _settings.Util.GetTestData("FacilityCodeHeaderValue"));
            _settings.lib.AddHeaderParameter(_settings.Request, _settings.ParameterList);
        }

        [When(@"User executes the GET request")]
        public void WhenUserExecutesTheGETRequest()
        {
            _settings.Request.RequestFormat = DataFormat.Json;
            _settings.Response = _settings.lib.ExecuteAsyncRequest<Model.Authorization>(_settings.RestClient, _settings.Request).GetAwaiter().GetResult();
          
        }

        [When(@"user get the response")]
        public void WhenUserGetTheResponse()
        {

            if (((RestSharp.RestResponse<PxTransformAutomation.Model.Authorization>)_settings.Response).Data.recordKeys.Count != 0)
            {
                actualResistrationIDs = ((RestSharp.RestResponse<PxTransformAutomation.Model.Authorization>)_settings.Response).Data.recordKeys.ToList();
                actualResistrationIDs.Sort();
            }
            else
            {
                Assert.True(((RestSharp.RestResponse<PxTransformAutomation.Model.Authorization>)_settings.Response).Data.recordKeys.Count != 0, " No reponse data between " + _settings.Util.GetTestData("MinDaysOut") + " And " + _settings.Util.GetTestData("MaxDaysOut") + " Days");
            }
        }

        [Then(@"user should get success status response code")]
        public void ThenUserShouldGetSuccessStatusResponseCode()
        {
           
            int ExpectedStatusCode = int.Parse(_settings.Util.GetTestData("ExpectedSatusCode"));
            Assert.True(((int)_settings.Response.StatusCode).Equals(ExpectedStatusCode), "Expected Status Code is:- " + ExpectedStatusCode + " But Found:- " + (int)_settings.Response.StatusCode);
        }


        [Then(@"user should get eligible accounts whose patienttype is not E in the response")]
        public void ThenUserShouldGetEligibleAccountsWhosePatienttypeIsNotEInTheResponse()
        {
            var authE = _dataCollector.GetRegisterInstance().GetAuthEligibleAccounts(_tranContext, _accretiveContext);
            //var auThSched = _dataCollector.GetRegisterInstance().GetEligibleAuthschedulerlog(_tranContext, actualResistrationIDs);

            List<int> regisID = new List<int>();
     

            regisID = _settings.lib.GetRegistrationIDlist(authE);

            Assert.True(actualResistrationIDs.Equals(regisID), "Expected Data " + regisID.Count + " But found:- " + actualResistrationIDs.Count);
        }


        [Given(@"user hits the AuthUri  with mindaysout and maxdaysout parameters")]
        public void GivenUserHitsTheAuthUriWithMindaysoutAndMaxdaysoutParameters()
        {
            //var regdata = _dataCollector.GetRegisterInstance().GetAuthEligibleAccounts(_tranContext);
            _settings.Request = _settings.lib.GetRequest("api/authorization?mindaysout=0&maxdaysout=5", Method.GET);
            
        }

        [Then(@"user should get eligible accounts whose coverage is not self pay in the response")]
        public void ThenUserShouldGetEligibleAccountsWhoseCoverageIsNotSelfPayInTheResponse()
        {
            var authE = _dataCollector.GetRegisterInstance().GetAuthEligibleAccounts(_tranContext, _accretiveContext);
          
            List<int> regisID = new List<int>();


            regisID = _settings.lib.GetRegistrationIDlist(authE);

            Assert.True(actualResistrationIDs.Equals(regisID), "Expected Data " + regisID.Count + " But found:- " + actualResistrationIDs.Count);
        }


        [When(@"Authschedulerlog should have entry for all the eligible accounts")]
        public void WhenAuthschedulerlogShouldHaveEntryForAllTheEligibleAccounts()
        {
            var authschedulerlog = _dataCollector.GetRegisterInstance().GetEligibleAuthschedulerlog(_tranContext, actualResistrationIDs);

            List<int> regisIDauthschedulerlog = new List<int>();


            regisIDauthschedulerlog = _settings.lib.GetRegistrationIDlist(authschedulerlog);

            Assert.True(actualResistrationIDs.Equals(regisIDauthschedulerlog), "Expected Data " + regisIDauthschedulerlog.Count + " But found:- " + actualResistrationIDs.Count);

        }

        [When(@"authrequestlog should have entry for all the eligible accounts")]
        public void WhenAuthrequestlogShouldHaveEntryForAllTheEligibleAccounts()
        {

            var authRequestLog = _dataCollector.GetRegisterInstance().GetEligibleAuthRequestLog(_tranContext, actualResistrationIDs);

            List<int> regisIDauthRequestLog = new List<int>();


            regisIDauthRequestLog = _settings.lib.GetRegistrationIDlist(authRequestLog);

            Assert.True(actualResistrationIDs.Equals(regisIDauthRequestLog), "Expected Data " + regisIDauthRequestLog.Count + " But found:- " + actualResistrationIDs.Count);

        }









    }
}
