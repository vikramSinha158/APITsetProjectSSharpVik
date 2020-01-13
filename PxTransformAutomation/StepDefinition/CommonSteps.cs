using PxTransformAutomation.Base;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PxTransformAutomation.Hooks
{
    [Binding]
    class CommonSteps
    {
        //Context injection
        private Settings _settings;
        public CommonSteps(Settings settings)
        {
            _settings = settings;
        }

        [Given(@"User authentication with following details")]
        public void GivenUserAuthenticationWithFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();

            _settings.RestClient.Authenticator = new HttpBasicAuthenticator((string)data.UserName, (string)data.Password);
        }
    }
}
